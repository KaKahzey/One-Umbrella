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
