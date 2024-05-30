--use the following script to generate database structure

Create database PROG7311_POEPart2;

CREATE TABLE Farmer(
	Username	VARCHAR(255) PRIMARY KEY NOT NULL,
	Password	VARCHAR(255) NOT NULL,
	FName		VARCHAR(255) NOT NULL,
	Surname		VARCHAR(255) NOT NULL
);

CREATE TABLE Employee(
	Username	VARCHAR(255) PRIMARY KEY NOT NULL,
	Password	VARCHAR(255) NOT NULL,
	FName		VARCHAR(255) NOT NULL,
	Surname		VARCHAR(255) NOT NULL
);

CREATE TABLE Product(
	Id			INT NOT NULL IDENTITY(1,1),
	Name		VARCHAR(255) NOT NULL,
	ProdType	VARCHAR(50) NOT NULL,
	ProdDate	DATE NOT NULL,
	FarmerUName	VARCHAR(255) FOREIGN KEY REFERENCES Farmer(Username)
);

SELECT * FROM  Employee;
SELECT * FROM  Farmer;
SELECT * FROM  Product;
