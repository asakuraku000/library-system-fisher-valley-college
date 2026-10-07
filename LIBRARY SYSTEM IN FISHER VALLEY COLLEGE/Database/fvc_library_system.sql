-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 07, 2026 at 11:25 AM
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
(2, 'ACC-1002', '978-0262046305', 'Introduction to Algorithms', 'Cormen et al.', 'MIT Press', '2022', 'Computer Science', 2, 2, 'Available', '2026-09-26'),
(3, 'ACC-1003', '978-1000000009', 'Computer Networking: A Top-Down Approach', 'James F. Kurose & Keith W. Ross', 'Pearson', '2021', 'Information Technology', 1, 1, 'Available', '2026-10-07'),
(4, 'ACC-1004', '978-1000079197', 'CompTIA A+ Certification All-in-One Exam Guide', 'Mike Meyers', 'McGraw-Hill', '2019', 'Information Technology', 4, 1, 'Available', '2026-10-07'),
(5, 'ACC-1005', '978-1000158380', 'Network+ Guide to Networks', 'Jill West, Tamara Dean & Jean Andrews', 'Cengage', '2021', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(6, 'ACC-1006', '978-1000237573', 'Operating System Concepts', 'Abraham Silberschatz, Peter B. Galvin & Greg Gagne', 'Wiley', '2018', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(7, 'ACC-1007', '978-1000316766', 'Modern Operating Systems', 'Andrew S. Tanenbaum & Herbert Bos', 'Pearson', '2014', 'Information Technology', 5, 5, 'Available', '2026-10-07'),
(8, 'ACC-1008', '978-1000395952', 'Computer Organization and Design', 'David A. Patterson & John L. Hennessy', 'Morgan Kaufmann', '2020', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(9, 'ACC-1009', '978-1000475142', 'The Linux Command Line', 'William Shotts', 'No Starch Press', '2019', 'Information Technology', 1, 0, 'Available', '2026-10-07'),
(10, 'ACC-1010', '978-1000554335', 'Linux Bible', 'Christopher Negus', 'Wiley', '2020', 'Information Technology', 5, 5, 'Available', '2026-10-07'),
(11, 'ACC-1011', '978-1000633528', 'CCNA 200-301 Official Cert Guide', 'Wendell Odom', 'Cisco Press', '2019', 'Information Technology', 2, 1, 'Available', '2026-10-07'),
(12, 'ACC-1012', '978-1000712711', 'Cybersecurity Essentials', 'Charles J. Brooks, Christopher Grow, Philip Craig & Donald Short', 'Sybex', '2018', 'Information Technology', 5, 5, 'Available', '2026-10-07'),
(13, 'ACC-1013', '978-1000791907', 'Security+ Guide to Network Security Fundamentals', 'Mark Ciampa', 'Cengage', '2021', 'Information Technology', 4, 0, 'Archived', '2026-10-07'),
(14, 'ACC-1014', '978-1000871098', 'Hacking: The Art of Exploitation', 'Jon Erickson', 'No Starch Press', '2008', 'Information Technology', 2, 1, 'Damaged', '2026-10-07'),
(15, 'ACC-1015', '978-1000950281', 'Web Design with HTML, CSS, JavaScript and jQuery', 'Jon Duckett', 'Wiley', '2014', 'Information Technology', 2, 2, 'Available', '2026-10-07'),
(16, 'ACC-1016', '978-1001029474', 'HTML and CSS: Design and Build Websites', 'Jon Duckett', 'Wiley', '2011', 'Information Technology', 4, 2, 'Available', '2026-10-07'),
(17, 'ACC-1017', '978-1001108667', 'JavaScript and JQuery: Interactive Front-End Web Development', 'Jon Duckett', 'Wiley', '2014', 'Information Technology', 2, 2, 'Available', '2026-10-07'),
(18, 'ACC-1018', '978-1001187853', 'Eloquent JavaScript', 'Marijn Haverbeke', 'No Starch Press', '2018', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(19, 'ACC-1019', '978-1001267043', 'You Don\'t Know JS Yet', 'Kyle Simpson', 'Independently Published', '2020', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(20, 'ACC-1020', '978-1001346236', 'Learning PHP, MySQL & JavaScript', 'Robin Nixon', 'O\'Reilly Media', '2021', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(21, 'ACC-1021', '978-1001425429', 'PHP and MySQL Web Development', 'Luke Welling & Laura Thomson', 'Addison-Wesley', '2016', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(22, 'ACC-1022', '978-1001504612', 'Head First HTML and CSS', 'Elisabeth Robson & Eric Freeman', 'O\'Reilly Media', '2012', 'Information Technology', 5, 0, 'Damaged', '2026-10-07'),
(23, 'ACC-1023', '978-1001583808', 'Cloud Computing: Concepts, Technology & Architecture', 'Thomas Erl', 'Prentice Hall', '2013', 'Information Technology', 4, 4, 'Available', '2026-10-07'),
(24, 'ACC-1024', '978-1001662992', 'The Phoenix Project', 'Gene Kim, Kevin Behr & George Spafford', 'IT Revolution Press', '2013', 'Information Technology', 4, 4, 'Available', '2026-10-07'),
(25, 'ACC-1025', '978-1001742182', 'Don\'t Make Me Think', 'Steve Krug', 'New Riders', '2014', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(26, 'ACC-1026', '978-1001821375', 'The Design of Everyday Things', 'Don Norman', 'Basic Books', '2013', 'Information Technology', 1, 1, 'Available', '2026-10-07'),
(27, 'ACC-1027', '978-1001900568', 'TCP/IP Illustrated, Volume 1', 'W. Richard Stevens', 'Addison-Wesley', '2011', 'Information Technology', 3, 3, 'Available', '2026-10-07'),
(28, 'ACC-1028', '978-1001979755', 'Introduction to Algorithms', 'Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest & Clifford Stein', 'MIT Press', '2022', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(29, 'ACC-1029', '978-1002058947', 'Clean Code', 'Robert C. Martin', 'Prentice Hall', '2008', 'Computer Science', 2, 2, 'Available', '2026-10-07'),
(30, 'ACC-1030', '978-1002138137', 'The Pragmatic Programmer', 'David Thomas & Andrew Hunt', 'Addison-Wesley', '2019', 'Computer Science', 4, 4, 'Available', '2026-10-07'),
(31, 'ACC-1031', '978-1002217320', 'Code Complete', 'Steve McConnell', 'Microsoft Press', '2004', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(32, 'ACC-1032', '978-1002296516', 'Design Patterns: Elements of Reusable Object-Oriented Software', 'Erich Gamma, Richard Helm, Ralph Johnson & John Vlissides', 'Addison-Wesley', '1994', 'Computer Science', 1, 0, 'Lost', '2026-10-07'),
(33, 'ACC-1033', '978-1002375709', 'Refactoring', 'Martin Fowler', 'Addison-Wesley', '2018', 'Computer Science', 4, 4, 'Available', '2026-10-07'),
(34, 'ACC-1034', '978-1002454893', 'Structure and Interpretation of Computer Programs', 'Harold Abelson & Gerald Jay Sussman', 'MIT Press', '1996', 'Computer Science', 5, 5, 'Available', '2026-10-07'),
(35, 'ACC-1035', '978-1002534083', 'Artificial Intelligence: A Modern Approach', 'Stuart Russell & Peter Norvig', 'Pearson', '2020', 'Computer Science', 2, 2, 'Available', '2026-10-07'),
(36, 'ACC-1036', '978-1002613276', 'Deep Learning', 'Ian Goodfellow, Yoshua Bengio & Aaron Courville', 'MIT Press', '2016', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(37, 'ACC-1037', '978-1002692462', 'Hands-On Machine Learning with Scikit-Learn, Keras & TensorFlow', 'Aurelien Geron', 'O\'Reilly Media', '2022', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(38, 'ACC-1038', '978-1002771655', 'Python Crash Course', 'Eric Matthes', 'No Starch Press', '2023', 'Computer Science', 5, 5, 'Available', '2026-10-07'),
(39, 'ACC-1039', '978-1002850848', 'Automate the Boring Stuff with Python', 'Al Sweigart', 'No Starch Press', '2019', 'Computer Science', 5, 4, 'Damaged', '2026-10-07'),
(40, 'ACC-1040', '978-1002930038', 'Fluent Python', 'Luciano Ramalho', 'O\'Reilly Media', '2022', 'Computer Science', 2, 2, 'Available', '2026-10-07'),
(41, 'ACC-1041', '978-1003009221', 'Learning Python', 'Mark Lutz', 'O\'Reilly Media', '2013', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(42, 'ACC-1042', '978-1003088417', 'Effective Java', 'Joshua Bloch', 'Addison-Wesley', '2018', 'Computer Science', 1, 1, 'Available', '2026-10-07'),
(43, 'ACC-1043', '978-1003167600', 'Java: The Complete Reference', 'Herbert Schildt', 'McGraw-Hill', '2021', 'Computer Science', 2, 1, 'Damaged', '2026-10-07'),
(44, 'ACC-1044', '978-1003246794', 'Head First Java', 'Kathy Sierra & Bert Bates', 'O\'Reilly Media', '2005', 'Computer Science', 2, 0, 'Available', '2026-10-07'),
(45, 'ACC-1045', '978-1003325987', 'The C Programming Language', 'Brian W. Kernighan & Dennis M. Ritchie', 'Prentice Hall', '1988', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(46, 'ACC-1046', '978-1003405177', 'C++ Primer', 'Stanley B. Lippman, Josee Lajoie & Barbara E. Moo', 'Addison-Wesley', '2012', 'Computer Science', 1, 0, 'Available', '2026-10-07'),
(47, 'ACC-1047', '978-1003484363', 'Programming: Principles and Practice Using C++', 'Bjarne Stroustrup', 'Addison-Wesley', '2014', 'Computer Science', 3, 0, 'Archived', '2026-10-07'),
(48, 'ACC-1048', '978-1003563556', 'Data Structures and Algorithms in Java', 'Michael T. Goodrich & Roberto Tamassia', 'Wiley', '2014', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(49, 'ACC-1049', '978-1003642749', 'Algorithms', 'Robert Sedgewick & Kevin Wayne', 'Addison-Wesley', '2011', 'Computer Science', 4, 4, 'Available', '2026-10-07'),
(50, 'ACC-1050', '978-1003721932', 'Grokking Algorithms', 'Aditya Bhargava', 'Manning', '2016', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(51, 'ACC-1051', '978-1003801122', 'Introduction to the Theory of Computation', 'Michael Sipser', 'Cengage', '2012', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(52, 'ACC-1052', '978-1003880318', 'Compilers: Principles, Techniques, and Tools', 'Alfred V. Aho, Monica S. Lam, Ravi Sethi & Jeffrey D. Ullman', 'Pearson', '2006', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(53, 'ACC-1053', '978-1003959502', 'Computer Networks', 'Andrew S. Tanenbaum & David J. Wetherall', 'Pearson', '2010', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(54, 'ACC-1054', '978-1004038695', 'Visual Basic 2019 How to Program', 'Paul Deitel & Harvey Deitel', 'Pearson', '2020', 'Computer Science', 1, 0, 'Available', '2026-10-07'),
(55, 'ACC-1055', '978-1004117888', 'Murach\'s Visual Basic 2015', 'Anne Boehm & Ged Mead', 'Mike Murach & Associates', '2016', 'Computer Science', 5, 5, 'Available', '2026-10-07'),
(56, 'ACC-1056', '978-1004197071', 'Programming in Visual Basic', 'Julia Case Bradley & Anita C. Millspaugh', 'McGraw-Hill', '2012', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(57, 'ACC-1057', '978-1004276264', 'Discrete Mathematics and Its Applications', 'Kenneth H. Rosen', 'McGraw-Hill', '2018', 'Computer Science', 3, 3, 'Available', '2026-10-07'),
(58, 'ACC-1058', '978-1004355457', 'Mining of Massive Datasets', 'Jure Leskovec, Anand Rajaraman & Jeffrey D. Ullman', 'Cambridge University Press', '2020', 'Computer Science', 1, 1, 'Available', '2026-10-07'),
(59, 'ACC-1059', '978-1004434640', 'Cracking the Coding Interview', 'Gayle Laakmann McDowell', 'CareerCup', '2015', 'Computer Science', 4, 4, 'Available', '2026-10-07'),
(60, 'ACC-1060', '978-1004513833', 'Database Systems: Design, Implementation, & Management', 'Carlos Coronel & Steven Morris', 'Cengage', '2022', 'Information Systems', 1, 0, 'Lost', '2026-10-07'),
(61, 'ACC-1061', '978-1004593026', 'Fundamentals of Database Systems', 'Ramez Elmasri & Shamkant B. Navathe', 'Pearson', '2015', 'Information Systems', 3, 2, 'Available', '2026-10-07'),
(62, 'ACC-1062', '978-1004672219', 'Database System Concepts', 'Abraham Silberschatz, Henry F. Korth & S. Sudarshan', 'McGraw-Hill', '2019', 'Information Systems', 1, 1, 'Available', '2026-10-07'),
(63, 'ACC-1063', '978-1004751402', 'Database Management Systems', 'Raghu Ramakrishnan & Johannes Gehrke', 'McGraw-Hill', '2002', 'Information Systems', 3, 1, 'Available', '2026-10-07'),
(64, 'ACC-1064', '978-1004830596', 'SQL in 10 Minutes, Sams Teach Yourself', 'Ben Forta', 'Sams', '2020', 'Information Systems', 3, 3, 'Available', '2026-10-07'),
(65, 'ACC-1065', '978-1004909780', 'Learning SQL', 'Alan Beaulieu', 'O\'Reilly Media', '2020', 'Information Systems', 2, 1, 'Available', '2026-10-07'),
(66, 'ACC-1066', '978-1004988976', 'Head First SQL', 'Lynn Beighley', 'O\'Reilly Media', '2007', 'Information Systems', 1, 1, 'Available', '2026-10-07'),
(67, 'ACC-1067', '978-1005068165', 'Designing Data-Intensive Applications', 'Martin Kleppmann', 'O\'Reilly Media', '2017', 'Information Systems', 1, 1, 'Available', '2026-10-07'),
(68, 'ACC-1068', '978-1005147358', 'Systems Analysis and Design', 'Alan Dennis, Barbara Haley Wixom & Roberta M. Roth', 'Wiley', '2018', 'Information Systems', 1, 1, 'Available', '2026-10-07'),
(69, 'ACC-1069', '978-1005226541', 'Systems Analysis and Design Methods', 'Jeffrey L. Whitten & Lonnie D. Bentley', 'McGraw-Hill', '2007', 'Information Systems', 2, 2, 'Available', '2026-10-07'),
(70, 'ACC-1070', '978-1005305734', 'Management Information Systems: Managing the Digital Firm', 'Kenneth C. Laudon & Jane P. Laudon', 'Pearson', '2020', 'Information Systems', 1, 0, 'Damaged', '2026-10-07'),
(71, 'ACC-1071', '978-1005384920', 'Information Systems Today', 'Joseph Valacich & Christoph Schneider', 'Pearson', '2018', 'Information Systems', 3, 3, 'Available', '2026-10-07'),
(72, 'ACC-1072', '978-1005464110', 'Essentials of Systems Analysis and Design', 'Joseph S. Valacich, Joey F. George & Jeffrey A. Hoffer', 'Pearson', '2015', 'Information Systems', 2, 2, 'Available', '2026-10-07'),
(73, 'ACC-1073', '978-1005543303', 'Software Engineering', 'Ian Sommerville', 'Pearson', '2015', 'Information Systems', 3, 3, 'Available', '2026-10-07'),
(74, 'ACC-1074', '978-1005622497', 'Software Engineering: A Practitioner\'s Approach', 'Roger S. Pressman & Bruce R. Maxim', 'McGraw-Hill', '2019', 'Information Systems', 5, 0, 'Archived', '2026-10-07'),
(75, 'ACC-1075', '978-1005701680', 'The Mythical Man-Month', 'Frederick P. Brooks Jr.', 'Addison-Wesley', '1995', 'Information Systems', 2, 1, 'Available', '2026-10-07'),
(76, 'ACC-1076', '978-1005780876', 'Agile Estimating and Planning', 'Mike Cohn', 'Prentice Hall', '2005', 'Information Systems', 3, 3, 'Available', '2026-10-07'),
(77, 'ACC-1077', '978-1005860066', 'Scrum: The Art of Doing Twice the Work in Half the Time', 'Jeff Sutherland', 'Crown Business', '2014', 'Information Systems', 2, 1, 'Available', '2026-10-07'),
(78, 'ACC-1078', '978-1005939250', 'Business Data Communications and Networking', 'Jerry FitzGerald & Alan Dennis', 'Wiley', '2019', 'Information Systems', 3, 2, 'Damaged', '2026-10-07'),
(79, 'ACC-1079', '978-1006018442', 'Data Science for Business', 'Foster Provost & Tom Fawcett', 'O\'Reilly Media', '2013', 'Information Systems', 5, 1, 'Available', '2026-10-07'),
(80, 'ACC-1080', '978-1006097638', 'Enterprise Resource Planning', 'Ellen F. Monk & Bret J. Wagner', 'Cengage', '2012', 'Information Systems', 2, 0, 'Lost', '2026-10-07'),
(81, 'ACC-1081', '978-1006176821', 'Educational Psychology', 'Anita Woolfolk', 'Pearson', '2019', 'Education', 1, 1, 'Available', '2026-10-07'),
(82, 'ACC-1082', '978-1006256011', 'Teaching with Style', 'Anthony F. Grasha', 'Alliance Publishers', '1996', 'Education', 3, 3, 'Available', '2026-10-07'),
(83, 'ACC-1083', '978-1006335204', 'Pedagogy of the Oppressed', 'Paulo Freire', 'Bloomsbury', '2000', 'Education', 5, 5, 'Available', '2026-10-07'),
(84, 'ACC-1084', '978-1006414398', 'Democracy and Education', 'John Dewey', 'Macmillan', '1916', 'Education', 2, 2, 'Available', '2026-10-07'),
(85, 'ACC-1085', '978-1006493584', 'Mindset: The New Psychology of Success', 'Carol S. Dweck', 'Random House', '2006', 'Education', 4, 4, 'Available', '2026-10-07'),
(86, 'ACC-1086', '978-1006572777', 'Teach Like a Champion 3.0', 'Doug Lemov', 'Jossey-Bass', '2021', 'Education', 1, 0, 'Available', '2026-10-07'),
(87, 'ACC-1087', '978-1006651960', 'The First Days of School', 'Harry K. Wong & Rosemary T. Wong', 'Harry K. Wong Publications', '2018', 'Education', 3, 1, 'Available', '2026-10-07'),
(88, 'ACC-1088', '978-1006731150', 'How Learning Works', 'Susan A. Ambrose et al.', 'Jossey-Bass', '2010', 'Education', 1, 1, 'Available', '2026-10-07'),
(89, 'ACC-1089', '978-1006810343', 'Make It Stick', 'Peter C. Brown, Henry L. Roediger III & Mark A. McDaniel', 'Harvard University Press', '2014', 'Education', 2, 0, 'Damaged', '2026-10-07'),
(90, 'ACC-1090', '978-1006889530', 'Visible Learning', 'John Hattie', 'Routledge', '2008', 'Education', 5, 5, 'Available', '2026-10-07'),
(91, 'ACC-1091', '978-1006968723', 'Understanding by Design', 'Grant Wiggins & Jay McTighe', 'ASCD', '2005', 'Education', 4, 4, 'Available', '2026-10-07'),
(92, 'ACC-1092', '978-1007047915', 'Frames of Mind: The Theory of Multiple Intelligences', 'Howard Gardner', 'Basic Books', '2011', 'Education', 2, 1, 'Available', '2026-10-07'),
(93, 'ACC-1093', '978-1007127105', 'Curriculum Development', 'Peter F. Oliva & William Gordon', 'Pearson', '2012', 'Education', 2, 1, 'Available', '2026-10-07'),
(94, 'ACC-1094', '978-1007206299', 'Child Development', 'Laura E. Berk', 'Pearson', '2012', 'Education', 5, 5, 'Available', '2026-10-07'),
(95, 'ACC-1095', '978-1007285485', 'Educational Research: Planning, Conducting, and Evaluating', 'John W. Creswell', 'Pearson', '2015', 'Education', 3, 1, 'Available', '2026-10-07'),
(96, 'ACC-1096', '978-1007364678', 'Teaching Today\'s Learners with Technology', 'Jeffrey Hsu', 'Routledge', '2020', 'Education', 1, 1, 'Available', '2026-10-07'),
(97, 'ACC-1097', '978-1007443861', 'Principles of Teaching 1', 'Gloria Evangelista & Rosalinda Dela Cruz', 'Rex Book Store', '2015', 'Education', 2, 2, 'Available', '2026-10-07'),
(98, 'ACC-1098', '978-1007523051', 'Assessment of Learning 1', 'Rosita L. Navarro & Rosita G. Santos', 'Lorimar Publishing', '2012', 'Education', 4, 3, 'Available', '2026-10-07'),
(99, 'ACC-1099', '978-1007602244', 'The Teacher\'s Guide to Classroom Management', 'Barbara Larrivee', 'Pearson', '2009', 'Education', 2, 2, 'Available', '2026-10-07'),
(100, 'ACC-1100', '978-1007681430', 'Principles of Management', 'Stephen P. Robbins & Mary Coulter', 'Pearson', '2018', 'Business Administration', 3, 3, 'Available', '2026-10-07'),
(101, 'ACC-1101', '978-1007760623', 'Marketing Management', 'Philip Kotler & Kevin Lane Keller', 'Pearson', '2015', 'Business Administration', 4, 4, 'Available', '2026-10-07'),
(102, 'ACC-1102', '978-1007839817', 'Principles of Marketing', 'Philip Kotler & Gary Armstrong', 'Pearson', '2020', 'Business Administration', 2, 2, 'Available', '2026-10-07'),
(103, 'ACC-1103', '978-1007919007', 'Financial Accounting', 'Carl S. Warren, Jim Reeve & Jonathan Duchac', 'Cengage', '2018', 'Business Administration', 2, 0, 'Archived', '2026-10-07'),
(104, 'ACC-1104', '978-1007998194', 'Fundamentals of Corporate Finance', 'Stephen A. Ross, Randolph W. Westerfield & Bradford D. Jordan', 'McGraw-Hill', '2019', 'Business Administration', 4, 2, 'Available', '2026-10-07'),
(105, 'ACC-1105', '978-1008077386', 'Organizational Behavior', 'Stephen P. Robbins & Timothy A. Judge', 'Pearson', '2018', 'Business Administration', 2, 2, 'Available', '2026-10-07'),
(106, 'ACC-1106', '978-1008156579', 'Entrepreneurship: Successfully Launching New Ventures', 'Bruce R. Barringer & R. Duane Ireland', 'Pearson', '2018', 'Business Administration', 1, 1, 'Available', '2026-10-07'),
(107, 'ACC-1107', '978-1008235762', 'Good to Great', 'Jim Collins', 'HarperBusiness', '2001', 'Business Administration', 3, 3, 'Available', '2026-10-07'),
(108, 'ACC-1108', '978-1008314955', 'The Lean Startup', 'Eric Ries', 'Crown Business', '2011', 'Business Administration', 2, 0, 'Available', '2026-10-07'),
(109, 'ACC-1109', '978-1008394148', 'Zero to One', 'Peter Thiel & Blake Masters', 'Crown Business', '2014', 'Business Administration', 2, 0, 'Available', '2026-10-07'),
(110, 'ACC-1110', '978-1008473331', 'The E-Myth Revisited', 'Michael E. Gerber', 'HarperBusiness', '1995', 'Business Administration', 3, 3, 'Available', '2026-10-07'),
(111, 'ACC-1111', '978-1008552524', 'Thinking, Fast and Slow', 'Daniel Kahneman', 'Farrar, Straus and Giroux', '2011', 'Business Administration', 4, 4, 'Available', '2026-10-07'),
(112, 'ACC-1112', '978-1008631717', 'The Five Dysfunctions of a Team', 'Patrick Lencioni', 'Jossey-Bass', '2002', 'Business Administration', 2, 2, 'Available', '2026-10-07'),
(113, 'ACC-1113', '978-1008710900', 'Rich Dad Poor Dad', 'Robert T. Kiyosaki', 'Warner Books', '1997', 'Business Administration', 2, 2, 'Available', '2026-10-07'),
(114, 'ACC-1114', '978-1008790094', 'Business Research Methods', 'Donald R. Cooper & Pamela S. Schindler', 'McGraw-Hill', '2013', 'Business Administration', 5, 4, 'Damaged', '2026-10-07'),
(115, 'ACC-1115', '978-1008869288', 'Human Resource Management', 'Gary Dessler', 'Pearson', '2019', 'Business Administration', 1, 1, 'Available', '2026-10-07'),
(116, 'ACC-1116', '978-1008948471', 'Operations Management', 'Jay Heizer, Barry Render & Chuck Munson', 'Pearson', '2019', 'Business Administration', 4, 4, 'Available', '2026-10-07'),
(117, 'ACC-1117', '978-1009027663', 'Microeconomics', 'Paul Krugman & Robin Wells', 'Worth Publishers', '2018', 'Business Administration', 5, 2, 'Available', '2026-10-07'),
(118, 'ACC-1118', '978-1009106856', 'Principles of Economics', 'N. Gregory Mankiw', 'Cengage', '2020', 'Business Administration', 3, 2, 'Available', '2026-10-07'),
(119, 'ACC-1119', '978-1009186049', 'Business Ethics: Ethical Decision Making and Cases', 'O.C. Ferrell, John Fraedrich & Linda Ferrell', 'Cengage', '2019', 'Business Administration', 2, 2, 'Available', '2026-10-07'),
(120, 'ACC-1120', '978-1009265232', 'Engineering Mechanics: Statics', 'Russell C. Hibbeler', 'Pearson', '2015', 'Engineering', 5, 4, 'Available', '2026-10-07'),
(121, 'ACC-1121', '978-1009344425', 'Engineering Mechanics: Dynamics', 'J.L. Meriam & L.G. Kraige', 'Wiley', '2015', 'Engineering', 5, 5, 'Available', '2026-10-07'),
(122, 'ACC-1122', '978-1009423618', 'Fundamentals of Electric Circuits', 'Charles K. Alexander & Matthew N. O. Sadiku', 'McGraw-Hill', '2016', 'Engineering', 5, 0, 'Available', '2026-10-07'),
(123, 'ACC-1123', '978-1009502801', 'Microelectronic Circuits', 'Adel S. Sedra & Kenneth C. Smith', 'Oxford University Press', '2014', 'Engineering', 4, 4, 'Available', '2026-10-07'),
(124, 'ACC-1124', '978-1009581998', 'The Art of Electronics', 'Paul Horowitz & Winfield Hill', 'Cambridge University Press', '2015', 'Engineering', 2, 2, 'Available', '2026-10-07'),
(125, 'ACC-1125', '978-1009661188', 'Digital Design', 'M. Morris Mano & Michael D. Ciletti', 'Pearson', '2018', 'Engineering', 4, 4, 'Available', '2026-10-07'),
(126, 'ACC-1126', '978-1009740371', 'Mechanics of Materials', 'Russell C. Hibbeler', 'Pearson', '2016', 'Engineering', 2, 1, 'Available', '2026-10-07'),
(127, 'ACC-1127', '978-1009819565', 'Fundamentals of Thermodynamics', 'Claus Borgnakke & Richard E. Sonntag', 'Wiley', '2019', 'Engineering', 2, 2, 'Available', '2026-10-07'),
(128, 'ACC-1128', '978-1009898751', 'Fluid Mechanics', 'Frank M. White', 'McGraw-Hill', '2015', 'Engineering', 3, 3, 'Available', '2026-10-07'),
(129, 'ACC-1129', '978-1009977944', 'Advanced Engineering Mathematics', 'Erwin Kreyszig', 'Wiley', '2011', 'Engineering', 3, 3, 'Available', '2026-10-07'),
(130, 'ACC-1130', '978-1010057130', 'Calculus: Early Transcendentals', 'James Stewart', 'Cengage', '2015', 'Engineering', 2, 2, 'Available', '2026-10-07'),
(131, 'ACC-1131', '978-1010136323', 'Engineering Economy', 'William G. Sullivan, Elin M. Wicks & C. Patrick Koelling', 'Pearson', '2014', 'Engineering', 2, 1, 'Available', '2026-10-07'),
(132, 'ACC-1132', '978-1010215516', 'Shigley\'s Mechanical Engineering Design', 'Richard G. Budynas & J. Keith Nisbett', 'McGraw-Hill', '2019', 'Engineering', 5, 5, 'Available', '2026-10-07'),
(133, 'ACC-1133', '978-1010294702', 'Structural Analysis', 'Russell C. Hibbeler', 'Pearson', '2017', 'Engineering', 3, 3, 'Available', '2026-10-07'),
(134, 'ACC-1134', '978-1010373896', 'Arduino Workshop', 'John Boxall', 'No Starch Press', '2021', 'Engineering', 1, 1, 'Available', '2026-10-07'),
(135, 'ACC-1135', '978-1010453086', 'Making Embedded Systems', 'Elecia White', 'O\'Reilly Media', '2011', 'Engineering', 2, 2, 'Available', '2026-10-07'),
(136, 'ACC-1136', '978-1010532279', 'Noli Me Tangere', 'Jose Rizal', 'Penguin Classics', '2006', 'Fiction', 1, 1, 'Available', '2026-10-07'),
(137, 'ACC-1137', '978-1010611462', 'El Filibusterismo', 'Jose Rizal', 'Penguin Classics', '2011', 'Fiction', 4, 4, 'Available', '2026-10-07'),
(138, 'ACC-1138', '978-1010690658', 'To Kill a Mockingbird', 'Harper Lee', 'J.B. Lippincott & Co.', '1960', 'Fiction', 3, 3, 'Available', '2026-10-07'),
(139, 'ACC-1139', '978-1010769842', '1984', 'George Orwell', 'Secker & Warburg', '1949', 'Fiction', 3, 3, 'Available', '2026-10-07'),
(140, 'ACC-1140', '978-1010849032', 'Animal Farm', 'George Orwell', 'Secker & Warburg', '1945', 'Fiction', 3, 0, 'Damaged', '2026-10-07'),
(141, 'ACC-1141', '978-1010928225', 'The Great Gatsby', 'F. Scott Fitzgerald', 'Charles Scribner\'s Sons', '1925', 'Fiction', 4, 4, 'Available', '2026-10-07'),
(142, 'ACC-1142', '978-1011007417', 'Pride and Prejudice', 'Jane Austen', 'Penguin Classics', '2002', 'Fiction', 2, 0, 'Archived', '2026-10-07'),
(143, 'ACC-1143', '978-1011086603', 'The Catcher in the Rye', 'J.D. Salinger', 'Little, Brown and Company', '1951', 'Fiction', 3, 2, 'Available', '2026-10-07'),
(144, 'ACC-1144', '978-1011165797', 'Harry Potter and the Sorcerer\'s Stone', 'J.K. Rowling', 'Scholastic', '1998', 'Fiction', 2, 2, 'Available', '2026-10-07'),
(145, 'ACC-1145', '978-1011244980', 'The Hobbit', 'J.R.R. Tolkien', 'George Allen & Unwin', '1937', 'Fiction', 3, 3, 'Available', '2026-10-07'),
(146, 'ACC-1146', '978-1011324170', 'The Lord of the Rings', 'J.R.R. Tolkien', 'George Allen & Unwin', '1954', 'Fiction', 1, 1, 'Available', '2026-10-07'),
(147, 'ACC-1147', '978-1011403363', 'The Alchemist', 'Paulo Coelho', 'HarperOne', '1993', 'Fiction', 3, 3, 'Available', '2026-10-07'),
(148, 'ACC-1148', '978-1011482559', 'The Little Prince', 'Antoine de Saint-Exupery', 'Reynal & Hitchcock', '1943', 'Fiction', 5, 5, 'Available', '2026-10-07'),
(149, 'ACC-1149', '978-1011561742', 'Lord of the Flies', 'William Golding', 'Faber and Faber', '1954', 'Fiction', 2, 1, 'Available', '2026-10-07'),
(150, 'ACC-1150', '978-1011640935', 'Brave New World', 'Aldous Huxley', 'Chatto & Windus', '1932', 'Fiction', 3, 0, 'Available', '2026-10-07'),
(151, 'ACC-1151', '978-1011720125', 'Fahrenheit 451', 'Ray Bradbury', 'Ballantine Books', '1953', 'Fiction', 3, 3, 'Available', '2026-10-07'),
(152, 'ACC-1152', '978-1011799312', 'The Hunger Games', 'Suzanne Collins', 'Scholastic Press', '2008', 'Fiction', 5, 0, 'Lost', '2026-10-07'),
(153, 'ACC-1153', '978-1011878505', 'Dune', 'Frank Herbert', 'Chilton Books', '1965', 'Fiction', 2, 0, 'Archived', '2026-10-07'),
(154, 'ACC-1154', '978-1011957699', 'Jane Eyre', 'Charlotte Bronte', 'Penguin Classics', '2006', 'Fiction', 2, 2, 'Available', '2026-10-07'),
(155, 'ACC-1155', '978-1012036881', 'Moby-Dick', 'Herman Melville', 'Penguin Classics', '2003', 'Fiction', 2, 1, 'Damaged', '2026-10-07'),
(156, 'ACC-1156', '978-1012116071', 'The Kite Runner', 'Khaled Hosseini', 'Riverhead Books', '2003', 'Fiction', 3, 2, 'Damaged', '2026-10-07'),
(157, 'ACC-1157', '978-1012195267', 'Dekada \'70', 'Lualhati Bautista', 'Anvil Publishing', '2005', 'Fiction', 1, 1, 'Available', '2026-10-07'),
(158, 'ACC-1158', '978-1012274450', 'Ang Mga Kuko ng Liwanag', 'Edgardo M. Reyes', 'Ateneo de Manila University Press', '1986', 'Fiction', 4, 3, 'Damaged', '2026-10-07'),
(159, 'ACC-1159', '978-1012353643', 'Ghostwritten', 'David Mitchell', 'Sceptre', '1999', 'Fiction', 3, 2, 'Damaged', '2026-10-07'),
(160, 'ACC-1160', '978-1012432836', 'Sapiens: A Brief History of Humankind', 'Yuval Noah Harari', 'Harper', '2015', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(161, 'ACC-1161', '978-1012512026', 'Atomic Habits', 'James Clear', 'Avery', '2018', 'Non-Fiction', 3, 0, 'Available', '2026-10-07'),
(162, 'ACC-1162', '978-1012591212', 'The 7 Habits of Highly Effective People', 'Stephen R. Covey', 'Free Press', '1989', 'Non-Fiction', 5, 5, 'Available', '2026-10-07'),
(163, 'ACC-1163', '978-1012670405', 'How to Win Friends and Influence People', 'Dale Carnegie', 'Simon & Schuster', '1936', 'Non-Fiction', 5, 5, 'Available', '2026-10-07'),
(164, 'ACC-1164', '978-1012749590', 'Educated', 'Tara Westover', 'Random House', '2018', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(165, 'ACC-1165', '978-1012828783', 'Man\'s Search for Meaning', 'Viktor E. Frankl', 'Beacon Press', '2006', 'Non-Fiction', 3, 3, 'Available', '2026-10-07'),
(166, 'ACC-1166', '978-1012907976', 'A Brief History of Time', 'Stephen Hawking', 'Bantam Books', '1988', 'Non-Fiction', 4, 4, 'Available', '2026-10-07'),
(167, 'ACC-1167', '978-1012987169', 'Cosmos', 'Carl Sagan', 'Random House', '1980', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(168, 'ACC-1168', '978-1013066351', 'Guns, Germs, and Steel', 'Jared Diamond', 'W. W. Norton', '1997', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(169, 'ACC-1169', '978-1013145544', 'The Diary of a Young Girl', 'Anne Frank', 'Contact Publishing', '1947', 'Non-Fiction', 5, 5, 'Available', '2026-10-07'),
(170, 'ACC-1170', '978-1013224737', 'Outliers: The Story of Success', 'Malcolm Gladwell', 'Little, Brown and Company', '2008', 'Non-Fiction', 5, 3, 'Available', '2026-10-07'),
(171, 'ACC-1171', '978-1013303920', 'Deep Work', 'Cal Newport', 'Grand Central Publishing', '2016', 'Non-Fiction', 4, 4, 'Available', '2026-10-07'),
(172, 'ACC-1172', '978-1013383113', 'Grit: The Power of Passion and Perseverance', 'Angela Duckworth', 'Scribner', '2016', 'Non-Fiction', 3, 3, 'Available', '2026-10-07'),
(173, 'ACC-1173', '978-1013462306', 'Quiet: The Power of Introverts', 'Susan Cain', 'Crown', '2012', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(174, 'ACC-1174', '978-1013541490', 'The Subtle Art of Not Giving a F*ck', 'Mark Manson', 'HarperOne', '2016', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(175, 'ACC-1175', '978-1013620683', 'Philippine History', 'Teodoro A. Agoncillo', 'Garotech Publishing', '1990', 'Non-Fiction', 2, 0, 'Available', '2026-10-07'),
(176, 'ACC-1176', '978-1013699870', 'The Philippines: A Past Revisited', 'Renato Constantino', 'Tala Publishing', '1975', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(177, 'ACC-1177', '978-1013779060', 'Becoming', 'Michelle Obama', 'Crown', '2018', 'Non-Fiction', 3, 3, 'Available', '2026-10-07'),
(178, 'ACC-1178', '978-1013858253', 'The Origin of Species', 'Charles Darwin', 'Penguin Classics', '2009', 'Non-Fiction', 2, 2, 'Available', '2026-10-07'),
(179, 'ACC-1179', '978-1013937446', 'Merriam-Webster\'s Collegiate Dictionary', 'Merriam-Webster', 'Merriam-Webster', '2020', 'Reference', 5, 5, 'Available', '2026-10-07'),
(180, 'ACC-1180', '978-1014016638', 'Oxford English Dictionary (Concise)', 'Angus Stevenson', 'Oxford University Press', '2011', 'Reference', 5, 5, 'Available', '2026-10-07'),
(181, 'ACC-1181', '978-1014095824', 'The Chicago Manual of Style', 'University of Chicago Press Editorial Staff', 'University of Chicago Press', '2017', 'Reference', 5, 2, 'Available', '2026-10-07'),
(182, 'ACC-1182', '978-1014175014', 'Publication Manual of the American Psychological Association', 'American Psychological Association', 'APA', '2019', 'Reference', 4, 4, 'Available', '2026-10-07'),
(183, 'ACC-1183', '978-1014254207', 'MLA Handbook', 'Modern Language Association', 'MLA', '2021', 'Reference', 4, 2, 'Available', '2026-10-07'),
(184, 'ACC-1184', '978-1014333391', 'The Elements of Style', 'William Strunk Jr. & E. B. White', 'Pearson', '1999', 'Reference', 2, 2, 'Available', '2026-10-07'),
(185, 'ACC-1185', '978-1014412584', 'Encyclopaedia Britannica Almanac', 'Encyclopaedia Britannica', 'Encyclopaedia Britannica', '2020', 'Reference', 3, 3, 'Available', '2026-10-07'),
(186, 'ACC-1186', '978-1014491770', 'Atlas of the World', 'National Geographic', 'National Geographic', '2019', 'Reference', 5, 0, 'Available', '2026-10-07'),
(187, 'ACC-1187', '978-1014570963', 'Roget\'s Thesaurus', 'Barbara Ann Kipfer', 'Collins', '2013', 'Reference', 5, 2, 'Available', '2026-10-07'),
(188, 'ACC-1188', '978-1014650153', 'The 1987 Constitution of the Republic of the Philippines', 'Joaquin G. Bernas', 'Rex Book Store', '2009', 'Reference', 2, 0, 'Lost', '2026-10-07'),
(189, 'ACC-1189', '978-1014729347', 'Black\'s Law Dictionary', 'Bryan A. Garner', 'Thomson Reuters', '2019', 'Reference', 2, 2, 'Available', '2026-10-07'),
(190, 'ACC-1190', '978-1014808530', 'Gray\'s Anatomy', 'Henry Gray', 'Elsevier', '2020', 'Reference', 3, 1, 'Available', '2026-10-07'),
(191, 'ACC-1191', '978-1014887726', 'CRC Handbook of Chemistry and Physics', 'John R. Rumble', 'CRC Press', '2021', 'Reference', 3, 3, 'Available', '2026-10-07'),
(192, 'ACC-1192', '978-1014966919', 'Statistical Abstract of the Philippines', 'Philippine Statistics Authority', 'PSA', '2020', 'Reference', 2, 1, 'Available', '2026-10-07'),
(193, 'ACC-1193', '978-1015046108', 'The Art of War', 'Sun Tzu', 'Shambhala', '2003', 'Other', 2, 1, 'Available', '2026-10-07'),
(194, 'ACC-1194', '978-1015125292', 'The Prince', 'Niccolo Machiavelli', 'Penguin Classics', '2003', 'Other', 1, 1, 'Available', '2026-10-07'),
(195, 'ACC-1195', '978-1015204485', 'Meditations', 'Marcus Aurelius', 'Modern Library', '2002', 'Other', 4, 0, 'Available', '2026-10-07'),
(196, 'ACC-1196', '978-1015283671', 'The Republic', 'Plato', 'Penguin Classics', '2007', 'Other', 2, 2, 'Available', '2026-10-07'),
(197, 'ACC-1197', '978-1015362864', 'Nicomachean Ethics', 'Aristotle', 'Cambridge University Press', '2000', 'Other', 4, 3, 'Available', '2026-10-07'),
(198, 'ACC-1198', '978-1015442054', 'The Story of Art', 'E. H. Gombrich', 'Phaidon', '1995', 'Other', 4, 2, 'Available', '2026-10-07'),
(199, 'ACC-1199', '978-1015521247', 'The Joy of Cooking', 'Irma S. Rombauer', 'Scribner', '2019', 'Other', 3, 3, 'Available', '2026-10-07'),
(200, 'ACC-1200', '978-1015600430', 'Basic Photography', 'Michael Langford', 'Focal Press', '2010', 'Other', 2, 1, 'Available', '2026-10-07'),
(201, 'ACC-1201', '978-1015679627', 'Music: An Appreciation', 'Roger Kamien', 'McGraw-Hill', '2017', 'Other', 2, 0, 'Lost', '2026-10-07'),
(202, 'ACC-1202', '978-1015758810', 'Physical Education and Health Fundamentals', 'Jocelyn Reyes', 'Rex Book Store', '2014', 'Other', 5, 5, 'Available', '2026-10-07'),
(203, 'ACC-1203', '978-1015838000', 'Introduction to Psychology', 'James W. Kalat', 'Cengage', '2016', 'Other', 3, 3, 'Available', '2026-10-07'),
(204, 'ACC-1204', '978-1015917194', 'Introduction to Sociology', 'OpenStax', 'OpenStax', '2015', 'Other', 2, 1, 'Available', '2026-10-07');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_categories`
--

CREATE TABLE `tbl_categories` (
  `category_id` int(11) NOT NULL,
  `category_name` varchar(100) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `date_added` date DEFAULT curdate()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_categories`
--

INSERT INTO `tbl_categories` (`category_id`, `category_name`, `description`, `date_added`) VALUES
(1, 'Information Technology', NULL, '2026-10-07'),
(2, 'Computer Science', NULL, '2026-10-07'),
(3, 'Information Systems', NULL, '2026-10-07'),
(4, 'Education', NULL, '2026-10-07'),
(5, 'Business Administration', NULL, '2026-10-07'),
(6, 'Engineering', NULL, '2026-10-07'),
(7, 'Fiction', NULL, '2026-10-07'),
(8, 'Non-Fiction', NULL, '2026-10-07'),
(9, 'Reference', NULL, '2026-10-07'),
(10, 'Other', NULL, '2026-10-07');

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
-- Table structure for table `tbl_settings`
--

CREATE TABLE `tbl_settings` (
  `setting_key` varchar(50) NOT NULL,
  `setting_value` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_settings`
--

INSERT INTO `tbl_settings` (`setting_key`, `setting_value`, `description`, `updated_at`) VALUES
('fine_per_day', '5.00', 'Overdue fine in PHP per day', '2026-10-07 07:06:17'),
('library_name', 'Fisher Valley College Library', 'Name shown on forms and reports', '2026-10-07 07:06:17'),
('loan_days', '14', 'Default loan period in days', '2026-10-07 07:06:17'),
('max_books_per_member', '3', 'Max books a member can borrow at once', '2026-10-07 07:06:17');

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
,`computed_fine` decimal(14,2)
);

-- --------------------------------------------------------

--
-- Structure for view `vw_overdue_books`
--
DROP TABLE IF EXISTS `vw_overdue_books`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_overdue_books`  AS SELECT `t`.`transaction_id` AS `transaction_id`, `b`.`accession_number` AS `accession_number`, `b`.`title` AS `title`, `m`.`full_name` AS `borrower_name`, `m`.`id_number` AS `id_number`, `m`.`contact_number` AS `contact_number`, `t`.`date_borrowed` AS `date_borrowed`, `t`.`due_date` AS `due_date`, to_days(curdate()) - to_days(`t`.`due_date`) AS `days_overdue`, (to_days(curdate()) - to_days(`t`.`due_date`)) * (select cast(`tbl_settings`.`setting_value` as decimal(8,2)) from `tbl_settings` where `tbl_settings`.`setting_key` = 'fine_per_day') AS `computed_fine` FROM ((`tbl_transactions` `t` join `tbl_books` `b` on(`t`.`book_id` = `b`.`book_id`)) join `tbl_members` `m` on(`t`.`member_id` = `m`.`member_id`)) WHERE `t`.`date_returned` is null AND `t`.`due_date` < curdate() ORDER BY to_days(curdate()) - to_days(`t`.`due_date`) DESC ;

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
-- Indexes for table `tbl_categories`
--
ALTER TABLE `tbl_categories`
  ADD PRIMARY KEY (`category_id`),
  ADD UNIQUE KEY `category_name` (`category_name`);

--
-- Indexes for table `tbl_members`
--
ALTER TABLE `tbl_members`
  ADD PRIMARY KEY (`member_id`),
  ADD UNIQUE KEY `id_number` (`id_number`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `tbl_settings`
--
ALTER TABLE `tbl_settings`
  ADD PRIMARY KEY (`setting_key`);

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
  MODIFY `book_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=205;

--
-- AUTO_INCREMENT for table `tbl_categories`
--
ALTER TABLE `tbl_categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

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
