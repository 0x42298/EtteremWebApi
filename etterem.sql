-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 01, 2026 at 10:26 AM
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
-- Database: `etterem`
--
CREATE DATABASE IF NOT EXISTS `etterem` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `etterem`;

-- --------------------------------------------------------

--
-- Table structure for table `rendeles`
--

CREATE TABLE `rendeles` (
  `id` int(11) NOT NULL,
  `dish` varchar(40) NOT NULL,
  `description` text NOT NULL,
  `orderTime` datetime NOT NULL,
  `updateTime` datetime NOT NULL,
  `vendegId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rendeles`
--

INSERT INTO `rendeles` (`id`, `dish`, `description`, `orderTime`, `updateTime`, `vendegId`) VALUES
(1, 'Gulyásleves', 'Extra csípős kérésre, kenyérrel.', '2025-05-02 12:30:00', '2025-05-02 12:30:00', 1),
(2, 'Rántott sajt', 'Hasábburgonyával és tartármártással.', '2025-05-14 19:15:00', '2025-05-14 19:15:00', 2),
(3, 'Halászlé', 'Szegedi módra, csípős paprikával.', '2025-05-26 13:00:00', '2025-05-26 13:00:00', 3),
(4, 'Túrós csusza', 'Tepertővel, dupla adag tejföllel.', '2025-06-08 20:40:00', '2025-06-08 20:40:00', 5),
(5, 'Somlói galuska', 'Desszert, extra csokiöntettel.', '2025-06-19 21:20:00', '2025-06-19 21:20:00', 6),
(6, 'goyim', 'goysauce', '2026-10-01 09:53:32', '2026-10-01 09:53:32', 1);

-- --------------------------------------------------------

--
-- Table structure for table `vendeg`
--

CREATE TABLE `vendeg` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `age` int(11) NOT NULL,
  `password` varchar(100) NOT NULL,
  `registrationTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `vendeg`
--

INSERT INTO `vendeg` (`id`, `name`, `email`, `age`, `password`, `registrationTime`) VALUES
(1, 'Németh Boglárka', 'nemeth.boglarka@example.com', 27, 'etterem1!', '2025-01-08 18:30:00'),
(2, 'Farkas Ábel', 'farkas.abel@example.com', 31, 'vacsora25', '2025-02-01 19:00:00'),
(3, 'Juhász Emese', 'juhasz.emese@example.com', 24, 'menu2025', '2025-02-19 20:15:00'),
(4, 'Orsós Kende', 'orsos.kende@example.com', 29, 'asztal12', '2025-03-10 17:45:00'),
(5, 'Rácz Hanna', 'racz.hanna@example.com', 26, 'pincer99', '2025-03-27 21:05:00'),
(6, 'Tamás Botond', 'tamas.botond@example.com', 33, 'foglalas7', '2025-04-15 18:50:00');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `rendeles`
--
ALTER TABLE `rendeles`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_rendeles_vendeg` (`vendegId`);

--
-- Indexes for table `vendeg`
--
ALTER TABLE `vendeg`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `rendeles`
--
ALTER TABLE `rendeles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `vendeg`
--
ALTER TABLE `vendeg`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `rendeles`
--
ALTER TABLE `rendeles`
  ADD CONSTRAINT `fk_rendeles_vendeg` FOREIGN KEY (`vendegId`) REFERENCES `vendeg` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
