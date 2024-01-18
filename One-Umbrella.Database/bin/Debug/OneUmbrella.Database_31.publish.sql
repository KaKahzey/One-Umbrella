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
USE [$(DatabaseName)];


GO
PRINT N'Dropping Unique Constraint [dbo].[ONLY_ONE_FRONT]...';


GO
ALTER TABLE [dbo].[ImageRestaurant] DROP CONSTRAINT [ONLY_ONE_FRONT];


GO
PRINT N'Dropping Check Constraint unnamed constraint on [dbo].[Human]...';


GO
ALTER TABLE [dbo].[Human] DROP CONSTRAINT [CK__Human__HUMAN_TYP__5AEE82B9];


GO
PRINT N'Dropping Check Constraint [dbo].[ENSURE_BIT]...';


GO
ALTER TABLE [dbo].[ImageRestaurant] DROP CONSTRAINT [ENSURE_BIT];


GO
PRINT N'Creating Check Constraint unnamed constraint on [dbo].[Human]...';


GO
ALTER TABLE [dbo].[Human] WITH NOCHECK
    ADD CHECK (HUMAN_TYPE IN ('Owner', 'Customer'));


GO
--HUMANS

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Gnorp', 'John', 'megatest@test.com', 'b45cffe084dd3d20d928bee85e7b0f21d4ca49decb7b02b45b6531b8bdc9998', '0476323655', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Noël', 'Brody', 'brody@gnorp.be', 'test14', '0032476322655', 'Customer')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Blop', 'Swish', 'string', 'b45cffe084dd3d20d928bee85e7b0f21d4ca49decb7b02b45b6531b8bdc9998', '0474565473', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Custo', 'Mer', 'custo@mer.be', 'hoii', '0475658495', 'Customer')

--

--RESTAURANTS

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION], [RESTAURANT_RATING])
VALUES (3, 'Vilber', 'rue de la street', 'Cité', '1495', 'restaurant pour manger', '3')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION], [RESTAURANT_RATING])
VALUES (1, 'Pasvilber', 'street of the rue', 'Céti', '4001', 'restaurant pour boire', '2')

INSERT INTO [dbo].Restaurant([HUMAN_ID], [RESTAURANT_NAME], [RESTAURANT_STREET], [RESTAURANT_CITY], [RESTAURANT_POSTCODE], [RESTAURANT_DESCRIPTION], [RESTAURANT_RATING])
VALUES (1, 'Aucun', 'pareil', 'same', '80085', 'nice nice', '5')

--

--GRIDS

INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
VALUES ('Main hall', '3', 100, 50)

INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
VALUES ('First floor', '3', 50, 30)

INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
VALUES ('Main hall', '2', 75, 62)

--

--RESERVATIONS

INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
VALUES (1, 2, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
VALUES (1, 4, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

	INSERT INTO dbo.Reservation(RESTAURANT_ID, HUMAN_ID, RESERVATION_TIME_START, RESERVATION_TIME_END, RESERVATION_STATUS) 
	VALUES (3, 4, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 1)

--

--TABLES

INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
VALUES (1, 15, 20, 15, 21, 6, 2)

INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
VALUES (1, 10, 40, 10, 40, 2, 1)

INSERT INTO dbo.TableEntity(GRID_ID, ROW_INDEX, COLUMN_INDEX, END_ROW_INDEX, END_COLUMN_INDEX, SEAT_CAPABILITY, TABLE_Type)
VALUES (3, 72, 60, 72, 60, 8, 3)

--

--ELEMENTS

INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
VALUES (1, 20, 20, 1)

INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
VALUES (3, 20, 20, 1)

INSERT INTO dbo.StructuralElement(GRID_ID, ROW_INDEX, COLUMN_INDEX, ELEMENT_TYPE) 
VALUES (1, 40, 10, 6)

--
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Human'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO