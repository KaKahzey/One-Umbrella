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