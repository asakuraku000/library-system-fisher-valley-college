-- ============================================================
-- LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
-- Migration: run ONCE on your existing fvc_library_system
-- (phpMyAdmin > fvc_library_system > SQL tab). Keeps all current
-- data (books, members incl. 'mingming', transactions).
-- Adds: tbl_categories (Categories menu), tbl_settings (Settings
-- menu), tbl_members.account_type (Student/Teacher, used by the new
-- Create Account screen), and rebuilds vw_overdue_books to read the
-- fine rate from tbl_settings.
-- (XAMPP uses MariaDB, which supports ADD COLUMN IF NOT EXISTS.)
-- ============================================================
USE fvc_library_system;

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

-- also pick up any category already typed into tbl_books
INSERT IGNORE INTO tbl_categories (category_name)
SELECT DISTINCT TRIM(category) FROM tbl_books
WHERE category IS NOT NULL AND TRIM(category) <> '';

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

-- Student / Teacher picked on the Create Account screen.
-- Existing non-admin accounts are marked 'Student' by default; change
-- any teacher manually (e.g. UPDATE tbl_members SET account_type='Teacher' WHERE id_number='...').
ALTER TABLE tbl_members
    ADD COLUMN IF NOT EXISTS account_type ENUM('Student', 'Teacher') NULL AFTER role;
UPDATE tbl_members SET account_type = 'Student' WHERE role = 'member' AND account_type IS NULL;

-- Rebuild the overdue view (no DEFINER, so it works on any machine)
DROP VIEW IF EXISTS vw_overdue_books;
CREATE VIEW vw_overdue_books AS
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
