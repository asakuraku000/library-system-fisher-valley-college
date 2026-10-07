-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 26, 2026 at 02:05 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `fvc_library_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_books`
--

CREATE TABLE `tbl_books` (
  `book_id` int(11) NOT NULL,
  `accession_number` varchar(20) NOT NULL,
  `isbn` varchar(20) DEFAULT NULL,
  `title` varchar(200) NOT NULL,
  `author` varchar(150) DEFAULT NULL,
  `publisher` varchar(150) DEFAULT NULL,
  `year_published` year(4) DEFAULT NULL,
  `category` varchar(100) DEFAULT NULL,
  `copies_total` int(11) NOT NULL DEFAULT 1,
  `copies_available` int(11) NOT NULL DEFAULT 1,
  `status` enum('Available','Damaged','Lost','Archived') NOT NULL DEFAULT 'Available',
  `date_added` date DEFAULT curdate()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_books`
--

INSERT INTO `tbl_books` (`book_id`, `accession_number`, `isbn`, `title`, `author`, `publisher`, `year_published`, `category`, `copies_total`, `copies_available`, `status`, `date_added`) VALUES
(1, 'ACC-1001', '978-0357673034', 'Database Systems: Design, Implementation', 'Coronel & Morris', 'Cengage', '2019', 'Information Technology', 3, 2, 'Available', '2026-09-26'),
(2, 'ACC-1002', '978-0262046305', 'Introduction to Algorithms', 'Cormen et al.', 'MIT Press', '2022', 'Computer Science', 2, 2, 'Available', '2026-09-26');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_members`
--

CREATE TABLE `tbl_members` (
  `member_id` int(11) NOT NULL,
  `id_number` varchar(20) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `course` varchar(100) DEFAULT NULL,
  `year_level` varchar(20) DEFAULT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `role` enum('admin','member') NOT NULL DEFAULT 'member',
  `address` varchar(255) DEFAULT NULL,
  `membership_status` enum('Active','Inactive','Suspended') NOT NULL DEFAULT 'Active',
  `date_registered` date DEFAULT curdate()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_members`
--

INSERT INTO `tbl_members` (`member_id`, `id_number`, `full_name`, `course`, `year_level`, `contact_number`, `email`, `username`, `password`, `role`, `address`, `membership_status`, `date_registered`) VALUES
(1, 'ADMIN-0001', 'Library Administrator', NULL, NULL, NULL, 'admin@fvc.edu.ph', 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin', NULL, 'Active', '2026-09-26'),
(2, '2023-0001', 'Juan Dela Cruz', 'BS Information Technology', '3rd Year', '09171234567', 'juan.delacruz@fvc.edu.ph', NULL, NULL, 'member', NULL, 'Active', '2026-09-26'),
(3, '2023-0002', 'Maria Santos', 'BS Computer Science', '2nd Year', '09181234567', 'maria.santos@fvc.edu.ph', NULL, NULL, 'member', NULL, 'Active', '2026-09-26'),
(4, 'PAS-21-242', 'MING MING', 'BSCS', '3A', '09955115167', NULL, 'mingming', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'member', NULL, 'Active', '2026-09-26');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_transactions`
--

CREATE TABLE `tbl_transactions` (
  `transaction_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `member_id` int(11) NOT NULL,
  `date_borrowed` date NOT NULL,
  `due_date` date NOT NULL,
  `date_returned` date DEFAULT NULL,
  `status` enum('Borrowed','Returned','Overdue') NOT NULL DEFAULT 'Borrowed',
  `fine_amount` decimal(8,2) NOT NULL DEFAULT 0.00,
  `fine_paid` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_transactions`
--

INSERT INTO `tbl_transactions` (`transaction_id`, `book_id`, `member_id`, `date_borrowed`, `due_date`, `date_returned`, `status`, `fine_amount`, `fine_paid`) VALUES
(1, 1, 1, '2026-09-01', '2026-09-08', NULL, 'Borrowed', 0.00, 0),
(2, 2, 2, '2026-09-10', '2026-09-17', NULL, 'Borrowed', 0.00, 0);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vw_overdue_books`
-- (See below for the actual view)
--
CREATE TABLE `vw_overdue_books` (
`transaction_id` int(11)
,`accession_number` varchar(20)
,`title` varchar(200)
,`borrower_name` varchar(100)
,`id_number` varchar(20)
,`contact_number` varchar(20)
,`date_borrowed` date
,`due_date` date
,`days_overdue` int(7)
,`computed_fine` decimal(9,2)
);

-- --------------------------------------------------------

--
-- Structure for view `vw_overdue_books`
--
DROP TABLE IF EXISTS `vw_overdue_books`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_overdue_books`  AS SELECT `t`.`transaction_id` AS `transaction_id`, `b`.`accession_number` AS `accession_number`, `b`.`title` AS `title`, `m`.`full_name` AS `borrower_name`, `m`.`id_number` AS `id_number`, `m`.`contact_number` AS `contact_number`, `t`.`date_borrowed` AS `date_borrowed`, `t`.`due_date` AS `due_date`, to_days(curdate()) - to_days(`t`.`due_date`) AS `days_overdue`, (to_days(curdate()) - to_days(`t`.`due_date`)) * 5.00 AS `computed_fine` FROM ((`tbl_transactions` `t` join `tbl_books` `b` on(`t`.`book_id` = `b`.`book_id`)) join `tbl_members` `m` on(`t`.`member_id` = `m`.`member_id`)) WHERE `t`.`date_returned` is null AND `t`.`due_date` < curdate() ORDER BY to_days(curdate()) - to_days(`t`.`due_date`) DESC ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_books`
--
ALTER TABLE `tbl_books`
  ADD PRIMARY KEY (`book_id`),
  ADD UNIQUE KEY `accession_number` (`accession_number`);

--
-- Indexes for table `tbl_members`
--
ALTER TABLE `tbl_members`
  ADD PRIMARY KEY (`member_id`),
  ADD UNIQUE KEY `id_number` (`id_number`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `tbl_transactions`
--
ALTER TABLE `tbl_transactions`
  ADD PRIMARY KEY (`transaction_id`),
  ADD KEY `fk_trans_book` (`book_id`),
  ADD KEY `fk_trans_member` (`member_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbl_books`
--
ALTER TABLE `tbl_books`
  MODIFY `book_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tbl_members`
--
ALTER TABLE `tbl_members`
  MODIFY `member_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tbl_transactions`
--
ALTER TABLE `tbl_transactions`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbl_transactions`
--
ALTER TABLE `tbl_transactions`
  ADD CONSTRAINT `fk_trans_book` FOREIGN KEY (`book_id`) REFERENCES `tbl_books` (`book_id`),
  ADD CONSTRAINT `fk_trans_member` FOREIGN KEY (`member_id`) REFERENCES `tbl_members` (`member_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
