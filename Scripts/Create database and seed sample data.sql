USE master
GO

CREATE DATABASE BNGPAPPhoneBook
GO

USE BNGPAPPhoneBook
GO

CREATE TABLE PhoneBook
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(52) NOT NULL,

	PRIMARY KEY(Id)
)
GO

CREATE TABLE PhoneBookEntry
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[PhoneBookId] INT NOT NULL,
	[Name] NVARCHAR(52) NOT NULL,
	[ContactNumber] NVARCHAR(10) NOT NULL,

	PRIMARY KEY([Id]),
	FOREIGN KEY([PhoneBookId]) REFERENCES [PhoneBook]([Id])
)
GO

INSERT INTO [PhoneBook] VALUES ('Papa Bengu'), ('ABSA Banking')
GO

INSERT INTO [PhoneBookEntry] VALUES
(1, 'Home', '0719378823'),
(1, 'Work', '0115218546'),
(2, 'Call Centre', '0115427859'),
(2, 'Sales', '0115427810'),
(2, 'Bonds', '0115427811')
GO