CREATE DATABASE CompanyDB;
USE CompanyDB;

CREATE TABLE Employees (
    Emp_ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Age INT,
    Address VARCHAR(200),
    Salary DECIMAL(10, 2),
    Department VARCHAR(100)
);
CREATE TABLE User (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Name VARCHAR(255) NOT NULL
);
INSERT INTO User (Email, Password, Name)
VALUES ('divyanshu@gmail.com', '1234', 'Divyanshu');
