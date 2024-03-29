﻿CREATE TABLE [dbo].[TableEntity]
(
	[TABLE_ID] INT PRIMARY KEY IDENTITY,
	[GRID_ID] INT,
	[ROW_INDEX] INT,
	[COLUMN_INDEX] INT,
	[END_ROW_INDEX] INT,
	[END_COLUMN_INDEX] INT,
	[SEAT_CAPABILITY] TINYINT,
	[TABLE_TYPE] TINYINT
	CONSTRAINT FK_TABLE_GRID
		FOREIGN KEY (GRID_ID)
		REFERENCES Grid(GRID_ID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
