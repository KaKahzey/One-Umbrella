﻿--HUMANS

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Gnorp', 'John', 'john@gnorp.be', 'test1234', '0476323655', 'Owner')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Noël', 'Brody', 'brody@gnorp.be', 'test14', '0032476322655', 'Customer')

INSERT INTO [dbo].[Human] ([HUMAN_LASTNAME], [HUMAN_FIRSTNAME], [HUMAN_MAIL], [HUMAN_PASSWORD], [HUMAN_PHONE_NUMBER], [HUMAN_TYPE])
VALUES ('Blop', 'Swish', 'swish@blop.be', 'ploplo', '0474565473', 'Owner')

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
VALUES ('Main hall', '1', 100, 50)

INSERT INTO dbo.Grid([GRID_NAME], [RESTAURANT_ID], [GRID_ROWS], [GRID_COLUMNS]) 
VALUES ('First floor', '1', 50, 30)

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