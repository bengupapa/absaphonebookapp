USE master
GO

CREATE DATABASE BNGPAPPhoneBook
GO

------------
-- TABLES --
------------

USE BNGPAPPhoneBook
GO

CREATE TABLE PhoneBooks
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(52) NOT NULL,

	CONSTRAINT PK_PhoneBooks_Id PRIMARY KEY(Id)
)
GO

CREATE TABLE PhoneBookEntries
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[PhoneBookId] INT NOT NULL,
	[Name] NVARCHAR(52) NOT NULL,
	[ContactNumber] NVARCHAR(10) NOT NULL,

	CONSTRAINT PK_PhoneBookEntries_Id PRIMARY KEY([Id]),
	FOREIGN KEY([PhoneBookId]) REFERENCES [PhoneBooks]([Id])
)
GO

-------------
-- INDEXES --
-------------

CREATE UNIQUE INDEX uidx_name_pb ON [PhoneBooks] ([Name]);
CREATE UNIQUE INDEX uidx_cnt_name_pbe ON [PhoneBookEntries] ([Name], [ContactNumber]);

---------------
-- SEED DATA --
---------------

INSERT INTO [PhoneBooks] VALUES ('Papa Bengu'), ('ABSA Banking')
GO

INSERT INTO [PhoneBookEntries] VALUES
(1, 'Home', '0719378823'),
(1, 'Work', '0115218546'),
(2, 'Call Centre', '0115427859'),
(2, 'Sales', '0115427810'),
(2, 'Bonds', '0115427811')
GO

