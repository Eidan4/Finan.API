# Database Management and Setup Guide

### ****Overview****
This document provides the necessary details and commands to create, configure, and manage the database for the system, including tables for Students, Courses, Grades, and Teachers.

---

### ****Technologies Utilized****
- **MySQL**: Database management system.
- **InnoDB**: Storage engine for efficient transaction handling.
- **UTF8MB4**: Character set to support a wide range of characters and emojis.

---

### ****Tables and Schemas****

#### 1. **Students Table**
This table stores the main data about students.

```sql
CREATE TABLE `students` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Gender` varchar(10) DEFAULT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Picture` varchar(500) DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `Documents` varchar(500) NOT NULL,
  `Password` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

---

#### 2. **Courses Table**
Contains information about courses available for students.

```sql
CREATE TABLE `courses` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text DEFAULT NULL,
  `Credits` int(11) NOT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

---

#### 3. **Grades Table**
Links students to their respective courses and grades.

```sql
CREATE TABLE `grades` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `StudentId` int(11) NOT NULL,
  `CourseId` int(11) NOT NULL,
  `Grade` decimal(5,2) NOT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `StudentId` (`StudentId`),
  KEY `CourseId` (`CourseId`),
  CONSTRAINT `grades_ibfk_1` FOREIGN KEY (`StudentId`) REFERENCES `students` (`Id`),
  CONSTRAINT `grades_ibfk_2` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

---

#### 4. **Teachers Table**
Stores teacher information.

```sql
CREATE TABLE `teachers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

---

### ****Setup Instructions****

#### Prerequisites
1. **MySQL Server**: Ensure MySQL Server is installed and running.
2. **Database Access**: You must have access to create and manage tables in the desired database.

#### Steps to Set Up the Database

1. **Create the Database**:
   ```sql
   CREATE DATABASE SchoolDB CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
   USE SchoolDB;
   ```

2. **Run Table Creation Scripts**:
   Execute the provided SQL scripts in the following order:
   - Students Table
   - Courses Table
   - Teachers Table
   - Grades Table

3. **Verify Foreign Key Constraints**:
   Ensure that the foreign key relationships in the `grades` table are correctly linked to `students` and `courses`.

#### Example Configuration
For applications interacting with the database, include a connection string like the following:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=finan;User=root;Password=;"
  }
}
```

---

### ****Notes****
- All timestamps (`CreatedDate` and `UpdatedDate`) are automatically managed by the database.
- Make sure the `utf8mb4` character set is supported by your MySQL version to ensure compatibility with all characters.
- Always back up your database before making changes.

