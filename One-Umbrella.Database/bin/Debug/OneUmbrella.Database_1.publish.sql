﻿/*
Deployment script for One-Umbrella

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "One-Umbrella"
:setvar DefaultFilePrefix "One-Umbrella"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating Table [dbo].[Cell]...';


GO
CREATE TABLE [dbo].[Cell] (
    [GRID_ID]      INT          NOT NULL,
    [ROW_INDEX]    INT          NOT NULL,
    [COLUMN_INDEX] INT          NOT NULL,
    [CELL_TYPE]    NVARCHAR (3) NULL,
    PRIMARY KEY CLUSTERED ([GRID_ID] ASC, [ROW_INDEX] ASC, [COLUMN_INDEX] ASC)
);


GO
PRINT N'Creating Table [dbo].[Favorite]...';


GO
CREATE TABLE [dbo].[Favorite] (
    [HUMAN_ID]      INT NOT NULL,
    [RESTAURANT_ID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([HUMAN_ID] ASC, [RESTAURANT_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[Grid]...';


GO
CREATE TABLE [dbo].[Grid] (
    [GRID_ID]       INT           IDENTITY (1, 1) NOT NULL,
    [GRID_NAME]     NVARCHAR (50) NOT NULL,
    [RESTAURANT_ID] INT           NULL,
    PRIMARY KEY CLUSTERED ([GRID_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[Human]...';


GO
CREATE TABLE [dbo].[Human] (
    [HUMAN_ID]               INT           IDENTITY (1, 1) NOT NULL,
    [HUMAN_LASTNAME]         NVARCHAR (50) NOT NULL,
    [HUMAN_FIRSTNAME]        NVARCHAR (50) NOT NULL,
    [HUMAN_MAIL]             NVARCHAR (50) NOT NULL,
    [HUMAN_PASSWORD]         NVARCHAR (50) NOT NULL,
    [HUMAN_PHONE_NUMBER]     NVARCHAR (20) NULL,
    [HUMAN_DATE_INSCRIPTION] DATETIME      NULL,
    [HUMAN_TYPE]             NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([HUMAN_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[ImageRestaurant]...';


GO
CREATE TABLE [dbo].[ImageRestaurant] (
    [IMAGE_ID]      INT             IDENTITY (1, 1) NOT NULL,
    [RESTAURANT_ID] INT             NOT NULL,
    [IMAGE_BLOB]    VARBINARY (MAX) NOT NULL,
    [IS_FRONT]      BIT             NULL,
    PRIMARY KEY CLUSTERED ([IMAGE_ID] ASC),
    CONSTRAINT [ONLY_ONE_FRONT] UNIQUE NONCLUSTERED ([RESTAURANT_ID] ASC, [IS_FRONT] ASC)
);


GO
PRINT N'Creating Table [dbo].[Reservation]...';


GO
CREATE TABLE [dbo].[Reservation] (
    [RESERVATION_ID]         INT      IDENTITY (1, 1) NOT NULL,
    [RESTAURANT_ID]          INT      NULL,
    [HUMAN_ID]               INT      NULL,
    [RESERVATION_TIME_START] DATETIME NOT NULL,
    [RESERVATION_TIME_END]   DATETIME NOT NULL,
    [RESERVATION_STATUS]     TINYINT  NULL,
    PRIMARY KEY CLUSTERED ([RESERVATION_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[ReservedCell]...';


GO
CREATE TABLE [dbo].[ReservedCell] (
    [RESERVATION_ID] INT NOT NULL,
    [GRID_ID]        INT NOT NULL,
    [ROW_INDEX]      INT NOT NULL,
    [COLUMN_INDEX]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RESERVATION_ID] ASC, [GRID_ID] ASC, [ROW_INDEX] ASC, [COLUMN_INDEX] ASC)
);


GO
PRINT N'Creating Table [dbo].[Restaurant]...';


GO
CREATE TABLE [dbo].[Restaurant] (
    [RESTAURANT_ID]          INT             IDENTITY (1, 1) NOT NULL,
    [HUMAN_ID]               INT             NULL,
    [RESTAURANT_NAME]        NVARCHAR (30)   NOT NULL,
    [RESTAURANT_STREET]      NVARCHAR (100)  NOT NULL,
    [RESTAURANT_CITY]        NVARCHAR (30)   NOT NULL,
    [RESTAURANT_POSTCODE]    VARCHAR (20)    NOT NULL,
    [RESTAURANT_DESCRIPTION] NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([RESTAURANT_ID] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Human]...';


GO
ALTER TABLE [dbo].[Human]
    ADD DEFAULT GETDATE() FOR [HUMAN_DATE_INSCRIPTION];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Reservation]...';


GO
ALTER TABLE [dbo].[Reservation]
    ADD DEFAULT 1 FOR [RESERVATION_STATUS];


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cell_Grid]...';


GO
ALTER TABLE [dbo].[Cell]
    ADD CONSTRAINT [FK_Cell_Grid] FOREIGN KEY ([GRID_ID]) REFERENCES [dbo].[Grid] ([GRID_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_FAVORITE_USER]...';


GO
ALTER TABLE [dbo].[Favorite]
    ADD CONSTRAINT [FK_FAVORITE_USER] FOREIGN KEY ([HUMAN_ID]) REFERENCES [dbo].[Human] ([HUMAN_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_FAVORITE_RESTAURANT]...';


GO
ALTER TABLE [dbo].[Favorite]
    ADD CONSTRAINT [FK_FAVORITE_RESTAURANT] FOREIGN KEY ([RESTAURANT_ID]) REFERENCES [dbo].[Restaurant] ([RESTAURANT_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_GRID_RESTAURANT]...';


GO
ALTER TABLE [dbo].[Grid]
    ADD CONSTRAINT [FK_GRID_RESTAURANT] FOREIGN KEY ([RESTAURANT_ID]) REFERENCES [dbo].[Restaurant] ([RESTAURANT_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESERVATION_RESTAURANT]...';


GO
ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_RESERVATION_RESTAURANT] FOREIGN KEY ([RESTAURANT_ID]) REFERENCES [dbo].[Restaurant] ([RESTAURANT_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESERVATION_USER]...';


GO
ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_RESERVATION_USER] FOREIGN KEY ([HUMAN_ID]) REFERENCES [dbo].[Human] ([HUMAN_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESERVED_CELL_RESERVATION]...';


GO
ALTER TABLE [dbo].[ReservedCell]
    ADD CONSTRAINT [FK_RESERVED_CELL_RESERVATION] FOREIGN KEY ([RESERVATION_ID]) REFERENCES [dbo].[Reservation] ([RESERVATION_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESERVEDCELL_CELL]...';


GO
ALTER TABLE [dbo].[ReservedCell]
    ADD CONSTRAINT [FK_RESERVEDCELL_CELL] FOREIGN KEY ([GRID_ID], [ROW_INDEX], [COLUMN_INDEX]) REFERENCES [dbo].[Cell] ([GRID_ID], [ROW_INDEX], [COLUMN_INDEX]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESTAURANT_OWNER]...';


GO
ALTER TABLE [dbo].[Restaurant]
    ADD CONSTRAINT [FK_RESTAURANT_OWNER] FOREIGN KEY ([HUMAN_ID]) REFERENCES [dbo].[Human] ([HUMAN_ID]);


GO
PRINT N'Creating Check Constraint unnamed constraint on [dbo].[Human]...';


GO
ALTER TABLE [dbo].[Human]
    ADD CHECK (HUMAN_TYPE IN ('Owner', 'Customer'));


GO
PRINT N'Creating Check Constraint [dbo].[ENSURE_BIT]...';


GO
ALTER TABLE [dbo].[ImageRestaurant]
    ADD CONSTRAINT [ENSURE_BIT] CHECK ([IS_FRONT] IN (0, 1));


GO
--HUMANs

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Gnorp', 'John', 'john@gnorp.be', 'test1234', '0476323655', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Noël', 'Brody', 'brody@gnorp.be', 'test14', '0032476322655', 'Customer')
--

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
