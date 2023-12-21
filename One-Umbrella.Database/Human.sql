﻿CREATE TABLE [dbo].[Human]
(
	[HUMAN_ID] INT PRIMARY KEY IDENTITY,
	[HUMAN_LASTNAME] NVARCHAR(50) NOT NULL,
	[HUMAN_FIRSTNAME] NVARCHAR(50) NOT NULL,
	[HUMAN_MAIL] NVARCHAR(50) NOT NULL,
	[HUMAN_PASSWORD] NVARCHAR(50) NOT NULL,
	[HUMAN_PHONE_NUMBER] NVARCHAR(20),
	[HUMAN_DATE_INSCRIPTION] DATETIME DEFAULT GETDATE(),
	[HUMAN_TYPE] NVARCHAR(20) CHECK (HUMAN_TYPE IN ('Owner', 'Customer')),
)
