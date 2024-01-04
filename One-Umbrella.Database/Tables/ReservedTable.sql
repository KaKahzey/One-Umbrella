﻿CREATE TABLE [dbo].[ReservedTable]
(
	[RESERVATION_ID] INT,
	[TABLE_ID] INT
	PRIMARY KEY (RESERVATION_ID, TABLE_ID)
	CONSTRAINT FK_RESERVEDTABLE_RESERVATION
		FOREIGN KEY (RESERVATION_ID)
		REFERENCES Reservation(RESERVATION_ID)
		ON DELETE CASCADE
	CONSTRAINT FK_RESERVEDTABLE_TABLE
		FOREIGN KEY (TABLE_ID)
		REFERENCES TableEntity(TABLE_ID)

)