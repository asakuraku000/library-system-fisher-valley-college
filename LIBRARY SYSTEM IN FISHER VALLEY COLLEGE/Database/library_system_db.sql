-- ============================================================
-- LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
-- Database Schema
-- Part: Kahleb — Database Tables / Connections, Overdue Books
-- Engine: MySQL (XAMPP / MySQL Workbench)
-- ============================================================

CREATE DATABASE IF NOT EXISTS fvc_library_system;
USE fvc_library_system;

-- ------------------------------------------------------------
-- Table: tbl_members
-- Student/faculty borrower records
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tbl_members (
    member_id        INT AUTO_INCREMENT PRIMARY KEY,
    id_number         VARCHAR(20)  NOT NULL UNIQUE,
    full_name         VARCHAR(100) NOT NULL,
    course            VARCHAR(100),
    year_level        VARCHAR(20),
    contact_number    VARCHAR(20),
    email             VARCHAR(100),
    username          VARCHAR(50)  UNIQUE,
    password          VARCHAR(255),
    role              ENUM('admin', 'member') NOT NULL DEFAULT 'member',
    account_type      ENUM('Student', 'Teacher') NULL,  -- chosen on the Create Account screen
    address           VARCHAR(255),
    membership_status ENUM('Active', 'Inactive', 'Suspended') NOT NULL DEFAULT 'Active',
    date_registered   DATE DEFAULT (CURRENT_DATE)
) ENGINE=InnoDB;

-- If you already ran this script before today and your tbl_members table
-- was created WITHOUT the address/membership_status columns above, run
-- this once (MySQL 8.0.19+ / recent MariaDB) to add them without losing
-- your data. Existing rows get membership_status = 'Active' by default:
-- ALTER TABLE tbl_members
--     ADD COLUMN IF NOT EXISTS address VARCHAR(255) AFTER role,
--     ADD COLUMN IF NOT EXISTS membership_status ENUM('Active', 'Inactive', 'Suspended')
--         NOT NULL DEFAULT 'Active' AFTER address;

-- If you already ran this script before today and your tbl_members table
-- was created WITHOUT the username/password columns above, run this once
-- (MySQL 8.0.19+ / recent MariaDB) to add them without losing your data:
-- ALTER TABLE tbl_members
--     ADD COLUMN IF NOT EXISTS username VARCHAR(50) UNIQUE AFTER email,
--     ADD COLUMN IF NOT EXISTS password VARCHAR(255) AFTER username;

-- If you already ran this script before today and your tbl_members table
-- was created WITHOUT the role column above, run this once to add it
-- without losing your data. Every existing row becomes 'member' by
-- default, so also run the UPDATE below for whichever account should be
-- the admin:
-- ALTER TABLE tbl_members
--     ADD COLUMN IF NOT EXISTS role ENUM('admin', 'member') NOT NULL DEFAULT 'member' AFTER password;
-- UPDATE tbl_members SET role = 'admin' WHERE username = 'admin';

-- ------------------------------------------------------------
-- Table: tbl_books
-- Book catalog
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tbl_books (
    book_id           INT AUTO_INCREMENT PRIMARY KEY,
    accession_number  VARCHAR(20)  NOT NULL UNIQUE,
    isbn              VARCHAR(20),
    title             VARCHAR(200) NOT NULL,
    author            VARCHAR(150),
    publisher         VARCHAR(150),
    year_published    YEAR,
    category          VARCHAR(100),
    copies_total      INT NOT NULL DEFAULT 1,
    copies_available  INT NOT NULL DEFAULT 1,
    status            ENUM('Available', 'Damaged', 'Lost', 'Archived') NOT NULL DEFAULT 'Available',
    date_added        DATE DEFAULT (CURRENT_DATE)
) ENGINE=InnoDB;

-- If you already ran this script before today and your tbl_books table
-- was created WITHOUT the isbn / year_published / status columns above,
-- run this once (MySQL 8.0.19+ / recent MariaDB) to add them without
-- losing your data. Existing rows get status = 'Available' by default:
-- ALTER TABLE tbl_books
--     ADD COLUMN IF NOT EXISTS isbn VARCHAR(20) AFTER accession_number,
--     ADD COLUMN IF NOT EXISTS year_published YEAR AFTER publisher,
--     ADD COLUMN IF NOT EXISTS status ENUM('Available', 'Damaged', 'Lost', 'Archived')
--         NOT NULL DEFAULT 'Available' AFTER copies_available;

-- ------------------------------------------------------------
-- Table: tbl_transactions
-- Borrow / return records — the core table behind Overdue Books
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tbl_transactions (
    transaction_id    INT AUTO_INCREMENT PRIMARY KEY,
    book_id           INT NOT NULL,
    member_id         INT NOT NULL,
    date_borrowed     DATE NOT NULL,
    due_date          DATE NOT NULL,
    date_returned     DATE NULL,
    status            ENUM('Borrowed', 'Returned', 'Overdue') NOT NULL DEFAULT 'Borrowed',
    fine_amount       DECIMAL(8,2) NOT NULL DEFAULT 0.00,
    fine_paid         TINYINT(1) NOT NULL DEFAULT 0,
    CONSTRAINT fk_trans_book   FOREIGN KEY (book_id)   REFERENCES tbl_books(book_id),
    CONSTRAINT fk_trans_member FOREIGN KEY (member_id) REFERENCES tbl_members(member_id)
) ENGINE=InnoDB;

-- ------------------------------------------------------------
-- Table: tbl_categories
-- Master list behind the "Categories" admin menu and the Category
-- dropdown on the Books screen. tbl_books.category still stores the
-- category NAME (text), so frmBooks keeps working unchanged.
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tbl_categories (
    category_id    INT AUTO_INCREMENT PRIMARY KEY,
    category_name  VARCHAR(100) NOT NULL UNIQUE,
    description    VARCHAR(255),
    date_added     DATE DEFAULT (CURRENT_DATE)
) ENGINE=InnoDB;

INSERT IGNORE INTO tbl_categories (category_name) VALUES
('Information Technology'), ('Computer Science'), ('Information Systems'),
('Education'), ('Business Administration'), ('Engineering'),
('Fiction'), ('Non-Fiction'), ('Reference'), ('Other');

-- ------------------------------------------------------------
-- Table: tbl_settings
-- Key/value store behind the "Settings" admin menu. Replaces the
-- hard-coded fine rate (5.00) and 14-day loan period that used to
-- live in the VB forms and the overdue view.
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tbl_settings (
    setting_key    VARCHAR(50)  NOT NULL PRIMARY KEY,
    setting_value  VARCHAR(255) NOT NULL,
    description    VARCHAR(255),
    updated_at     TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

INSERT IGNORE INTO tbl_settings (setting_key, setting_value, description) VALUES
('library_name',        'Fisher Valley College Library', 'Name shown on forms and reports'),
('fine_per_day',        '5.00',                          'Overdue fine in PHP per day'),
('loan_days',           '14',                            'Default loan period in days'),
('max_books_per_member','3',                             'Max books a member can borrow at once');

-- ------------------------------------------------------------
-- View: vw_overdue_books
-- Computes overdue books live (no manual "mark overdue" step needed).
-- Fine rate now comes from tbl_settings ('fine_per_day'), editable
-- from the Settings screen instead of editing this view.
-- ------------------------------------------------------------
CREATE OR REPLACE VIEW vw_overdue_books AS
SELECT
    t.transaction_id,
    b.accession_number,
    b.title,
    m.full_name AS borrower_name,
    m.id_number,
    m.contact_number,
    t.date_borrowed,
    t.due_date,
    DATEDIFF(CURDATE(), t.due_date) AS days_overdue,
    DATEDIFF(CURDATE(), t.due_date) *
        (SELECT CAST(setting_value AS DECIMAL(8,2)) FROM tbl_settings WHERE setting_key = 'fine_per_day') AS computed_fine
FROM tbl_transactions t
INNER JOIN tbl_books b   ON t.book_id = b.book_id
INNER JOIN tbl_members m ON t.member_id = m.member_id
WHERE t.date_returned IS NULL
  AND t.due_date < CURDATE()
ORDER BY days_overdue DESC;

-- ------------------------------------------------------------
-- Seed admin account — the ONLY account that gets role='admin'.
-- Everyone who signs up through the app's "Create Account" screen
-- is inserted as role='member' (see createaccount.vb); admins are
-- never self-registered, so this seed row (or the UPDATE above) is
-- how you make the first one.
--   username: admin
--   password: Admin@2026   (change this after your first login —
--   there's no "change password" screen yet, so for now that means
--   updating the `password` column here with a new SHA-256 hash)
-- The hash below is SHA-256("Admin@2026"), lowercase hex, matching
-- Modules/AuthHelper.vb's HashPassword function.
-- ------------------------------------------------------------
INSERT INTO tbl_members (id_number, full_name, course, year_level, contact_number, email, username, password, role) VALUES
('ADMIN-0001', 'Library Administrator', NULL, NULL, NULL, 'admin@fvc.edu.ph',
 'admin', 'a36aef5a11c4073fbe60314fc9df530a9d5f986533594d1f5190742ff9e0e408', 'admin');

-- ------------------------------------------------------------
-- Sample data — lets you test the Overdue Books screen immediately
-- ------------------------------------------------------------
INSERT INTO tbl_members (id_number, full_name, course, year_level, contact_number, email) VALUES
('2023-0001', 'Juan Dela Cruz', 'BS Information Technology', '3rd Year', '09171234567', 'juan.delacruz@fvc.edu.ph'),
('2023-0002', 'Maria Santos',   'BS Computer Science',        '2nd Year', '09181234567', 'maria.santos@fvc.edu.ph');

UPDATE tbl_members SET account_type = 'Student' WHERE role = 'member' AND account_type IS NULL;

INSERT INTO tbl_books (accession_number, isbn, title, author, publisher, year_published, category, copies_total, copies_available, status) VALUES
('ACC-1001', '978-0357673034', 'Database Systems: Design, Implementation', 'Coronel & Morris', 'Cengage',   2019, 'Information Technology', 3, 2, 'Available'),
('ACC-1002', '978-0262046305', 'Introduction to Algorithms',                'Cormen et al.',    'MIT Press', 2022, 'Computer Science',       2, 2, 'Available');

-- Both records below are already past due as of a late-September 2026 test run
INSERT INTO tbl_transactions (book_id, member_id, date_borrowed, due_date, date_returned, status) VALUES
(1, 1, '2026-09-01', '2026-09-08', NULL, 'Borrowed'),
(2, 2, '2026-09-10', '2026-09-17', NULL, 'Borrowed');
