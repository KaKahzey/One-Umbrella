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
    [GRID_ROWS]     INT           NULL,
    [GRID_COLUMNS]  INT           NULL,
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
    [HUMAN_PASSWORD]         VARCHAR (MAX) NOT NULL,
    [HUMAN_PHONE_NUMBER]     NVARCHAR (20) NULL,
    [HUMAN_DATE_INSCRIPTION] DATETIME      NULL,
    [HUMAN_TYPE]             NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([HUMAN_ID] ASC),
    UNIQUE NONCLUSTERED ([HUMAN_MAIL] ASC),
    UNIQUE NONCLUSTERED ([HUMAN_PHONE_NUMBER] ASC)
);


GO
PRINT N'Creating Table [dbo].[ImageRestaurant]...';


GO
CREATE TABLE [dbo].[ImageRestaurant] (
    [IMAGE_ID]      INT             IDENTITY (1, 1) NOT NULL,
    [RESTAURANT_ID] INT             NOT NULL,
    [IMAGE_DATA]    VARBINARY (MAX) NOT NULL,
    [IS_FRONT]      BIT             NULL,
    [IS_MENU]       BIT             NULL,
    PRIMARY KEY CLUSTERED ([IMAGE_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[Rating]...';


GO
CREATE TABLE [dbo].[Rating] (
    [HUMAN_ID]      INT            NOT NULL,
    [RESTAURANT_ID] INT            NOT NULL,
    [SCORE]         TINYINT        NULL,
    [COMMENT]       NVARCHAR (100) NULL,
    CONSTRAINT [PK_HUMAN_RESTAURANT] PRIMARY KEY CLUSTERED ([HUMAN_ID] ASC, [RESTAURANT_ID] ASC)
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
PRINT N'Creating Table [dbo].[ReservedTable]...';


GO
CREATE TABLE [dbo].[ReservedTable] (
    [RESERVATION_ID] INT NOT NULL,
    [TABLE_ID]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RESERVATION_ID] ASC, [TABLE_ID] ASC)
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
    [RESTAURANT_RATING]      DECIMAL (5, 2)  NULL,
    PRIMARY KEY CLUSTERED ([RESTAURANT_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[StructuralElement]...';


GO
CREATE TABLE [dbo].[StructuralElement] (
    [ELEMENT_ID]   INT     IDENTITY (1, 1) NOT NULL,
    [GRID_ID]      INT     NULL,
    [ROW_INDEX]    INT     NULL,
    [COLUMN_INDEX] INT     NULL,
    [ELEMENT_TYPE] TINYINT NULL,
    PRIMARY KEY CLUSTERED ([ELEMENT_ID] ASC)
);


GO
PRINT N'Creating Table [dbo].[TableEntity]...';


GO
CREATE TABLE [dbo].[TableEntity] (
    [TABLE_ID]         INT     IDENTITY (1, 1) NOT NULL,
    [GRID_ID]          INT     NULL,
    [ROW_INDEX]        INT     NULL,
    [COLUMN_INDEX]     INT     NULL,
    [END_ROW_INDEX]    INT     NULL,
    [END_COLUMN_INDEX] INT     NULL,
    [SEAT_CAPABILITY]  TINYINT NULL,
    [TABLE_TYPE]       TINYINT NULL,
    PRIMARY KEY CLUSTERED ([TABLE_ID] ASC)
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
    ADD DEFAULT 0 FOR [RESERVATION_STATUS];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Restaurant]...';


GO
ALTER TABLE [dbo].[Restaurant]
    ADD DEFAULT 0 FOR [RESTAURANT_RATING];


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
PRINT N'Creating Foreign Key [dbo].[FK_IMAGE_RESTAURANT]...';


GO
ALTER TABLE [dbo].[ImageRestaurant]
    ADD CONSTRAINT [FK_IMAGE_RESTAURANT] FOREIGN KEY ([RESTAURANT_ID]) REFERENCES [dbo].[Restaurant] ([RESTAURANT_ID]);


GO
PRINT N'Creating Foreign Key [dbo].[RATING_HUMAN]...';


GO
ALTER TABLE [dbo].[Rating]
    ADD CONSTRAINT [RATING_HUMAN] FOREIGN KEY ([HUMAN_ID]) REFERENCES [dbo].[Human] ([HUMAN_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[RATING_RESTAURANT]...';


GO
ALTER TABLE [dbo].[Rating]
    ADD CONSTRAINT [RATING_RESTAURANT] FOREIGN KEY ([RESTAURANT_ID]) REFERENCES [dbo].[Restaurant] ([RESTAURANT_ID]) ON DELETE CASCADE;


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
PRINT N'Creating Foreign Key [dbo].[FK_RESERVEDTABLE_RESERVATION]...';


GO
ALTER TABLE [dbo].[ReservedTable]
    ADD CONSTRAINT [FK_RESERVEDTABLE_RESERVATION] FOREIGN KEY ([RESERVATION_ID]) REFERENCES [dbo].[Reservation] ([RESERVATION_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESERVEDTABLE_TABLE]...';


GO
ALTER TABLE [dbo].[ReservedTable]
    ADD CONSTRAINT [FK_RESERVEDTABLE_TABLE] FOREIGN KEY ([TABLE_ID]) REFERENCES [dbo].[TableEntity] ([TABLE_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_RESTAURANT_OWNER]...';


GO
ALTER TABLE [dbo].[Restaurant]
    ADD CONSTRAINT [FK_RESTAURANT_OWNER] FOREIGN KEY ([HUMAN_ID]) REFERENCES [dbo].[Human] ([HUMAN_ID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ELEMENT_GRID]...';


GO
ALTER TABLE [dbo].[StructuralElement]
    ADD CONSTRAINT [FK_ELEMENT_GRID] FOREIGN KEY ([GRID_ID]) REFERENCES [dbo].[Grid] ([GRID_ID]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_TABLE_GRID]...';


GO
ALTER TABLE [dbo].[TableEntity]
    ADD CONSTRAINT [FK_TABLE_GRID] FOREIGN KEY ([GRID_ID]) REFERENCES [dbo].[Grid] ([GRID_ID]);


GO
PRINT N'Creating Check Constraint unnamed constraint on [dbo].[Human]...';


GO
ALTER TABLE [dbo].[Human]
    ADD CHECK (HUMAN_TYPE IN ('Owner', 'Customer'));


GO
--HUMANS

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Dupont', 'Marie', 'marie.dupont@email.com', 'P@ssw0rd1', '+33 6 12 34 56 78', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Müller', 'Andreas', 'andreas.muller@email.com', 'SecurePass123', '+49 1512 3456789', 'Customer')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Rossi', 'Sofia', 'sofia.rossi@email.com', 'StrongPWD987!', '+39 333 1234567', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Custo', 'Mer', 'custo@mer.be', 'hoii', '0475658495', 'Customer')

--

--RESTAURANTS

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (1, 'Le Bistrot Parisien', '24 Rue de la Paix', 'Paris', '75002', 'Niché au cœur de Paris, Le Bistrot Parisien offre une expérience culinaire authentique avec des plats français classiques. La décoration élégante et l''ambiance chaleureuse font de cet endroit un lieu idéal pour savourer une délicieuse cuisine française, allant de coq au vin à la crème brûlée.')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (1, 'La Trattoria Deliziosa', '42 Via della Pace', 'Rome ', '00186', 'La Trattoria Deliziosa est un joyau gastronomique au cœur de Rome, proposant une cuisine italienne traditionnelle. Avec une atmosphère accueillante, le restaurant est réputé pour ses pâtes fraîches, ses pizzas authentiques et ses tiramisus exquis, offrant aux convives une véritable expérience culinaire italienne.')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (2, 'El Asador Argentino', 'Av. de Mayo 899', 'Buenos Aires', 'C1084ABB', 'Situé à Buenos Aires, El Asador Argentino est le paradis des amateurs de viande. Offrant un cadre rustique et chaleureux, le restaurant est célèbre pour ses grillades argentines, notamment ses succulents steaks de bœuf accompagnés de chimichurri, offrant une expérience authentique de la cuisine argentine.')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (2, 'Tokyo Sushi Haven', '3-8-2 Shibuya', 'Tokyo ', '150-0002', 'Tokyo Sushi Haven est une oasis de saveurs japonaises au cœur de Shibuya. Offrant une ambiance moderne et élégante, le restaurant propose une variété de sushis exquis, des sashimis délicats et des plats de tempura croustillants, offrant aux convives une expérience culinaire japonaise inoubliable.')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (2, 'Marrakech Spice Palace', '56 Rue Riad Zitoun Lakdim', 'Marrakech', '40000', 'Marrakech Spice Palace vous transporte dans un monde de saveurs marocaines. Avec une décoration envoûtante, le restaurant propose des tajines parfumés, des couscous délicats et des pâtisseries sucrées, offrant aux convives une immersion totale dans la cuisine traditionnelle marocaine.')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION])
VALUES (2, 'Seoul BBQ Delight', '17-6, Myeongdong 7-gil', 'Jung-gu', 'Séoul 04537', 'Seoul BBQ Delight offre une expérience de barbecue coréen authentique au cœur de Séoul. Avec des tables équipées de grills, les convives peuvent savourer une variété de viandes grillées, accompagnées de banchan délicieux, offrant une expérience culinaire immersive dans la tradition coréenne.')
--

--GRIDS

--INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
--VALUES ('Main hall', '3', 100, 50)

--INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
--VALUES ('First floor', '3', 50, 30)

--INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
--VALUES ('Main hall', '2', 75, 62)

--

--RESERVATIONS

--INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
--VALUES (1, 2, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

--INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
--VALUES (1, 4, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

--	INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
--	VALUES (3, 4, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

--

--TABLES

--INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
--VALUES (1, 15, 20, 15, 21, 6, 2)

--INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
--VALUES (1, 10, 40, 10, 40, 2, 1)

--INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
--VALUES (3, 72, 60, 72, 60, 8, 3)

--

--ELEMENTS

--INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
--VALUES (1, 20, 20, 1)

--INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
--VALUES (3, 20, 20, 1)

--INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
--VALUES (1, 40, 10, 6)

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
