/*
  FinFlow / InvoiceLedger database for Microsoft SQL Server.
  Run this script in SQL Server Management Studio before starting the application.
*/
IF DB_ID(N'InvoiceLedgerDb') IS NULL
    CREATE DATABASE InvoiceLedgerDb;
GO

USE InvoiceLedgerDb;
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
        Email        NVARCHAR(256) NOT NULL,
        PasswordHash NVARCHAR(64) NOT NULL,
        FullName     NVARCHAR(200) NOT NULL,
        CONSTRAINT UQ_Users_Email UNIQUE (Email)
    );
END;
GO

IF OBJECT_ID(N'dbo.Invoices', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Invoices
    (
        Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Invoices PRIMARY KEY,
        Number       NVARCHAR(50) NOT NULL,
        Counterparty NVARCHAR(200) NOT NULL,
        DueDate      DATETIME2 NOT NULL,
        Amount       DECIMAL(18,2) NOT NULL,
        Status       NVARCHAR(50) NOT NULL
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = N'admin@finflow.local')
BEGIN
    INSERT INTO dbo.Users (Email, PasswordHash, FullName)
    VALUES (N'admin@finflow.local', N'3EB3FE66B31E3B4D10FA70B5CAD49C7112294AF6AE4E476A1C405155D45AA121', N'Администратор');
END;
GO
