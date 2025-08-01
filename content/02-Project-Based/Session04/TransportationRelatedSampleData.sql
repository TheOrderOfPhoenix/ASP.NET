USE [AlibabaCloneDB]
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [Title]) VALUES (8, N'Dubai')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (5, N'Istanbul')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (2, N'London')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (1, N'New York')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (6, N'Paris')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (7, N'Sydney')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (4, N'Tehran')
INSERT [dbo].[Cities] ([Id], [Title]) VALUES (3, N'Tokyo')
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[LocationTypes] ON 

INSERT [dbo].[LocationTypes] ([Id], [Title]) VALUES (3, N'Airport')
INSERT [dbo].[LocationTypes] ([Id], [Title]) VALUES (1, N'Bus Terminal')
INSERT [dbo].[LocationTypes] ([Id], [Title]) VALUES (5, N'Metro Station')
INSERT [dbo].[LocationTypes] ([Id], [Title]) VALUES (4, N'Seaport')
INSERT [dbo].[LocationTypes] ([Id], [Title]) VALUES (2, N'Train Station')
SET IDENTITY_INSERT [dbo].[LocationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (1, N'JFK International Airport', 1, 3)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (2, N'New York Port Authority', 1, 1)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (3, N'London Heathrow Airport', 2, 3)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (4, N'Istanbul Bus Terminal', 5, 1)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (5, N'Tehran Railway Station', 4, 2)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (6, N'Paris Gare du Nord', 6, 2)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (7, N'Tokyo Metro Shibuya Station', 3, 5)
INSERT [dbo].[Locations] ([Id], [Title], [CityId], [LocationTypeId]) VALUES (8, N'Sydney Seaport', 7, 4)
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([Id], [Title]) VALUES (6, N'British Airways')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (5, N'Emirates')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (2, N'FlixBus')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (1, N'Greyhound')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (4, N'JR Central')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (7, N'Royal Caribbean')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (3, N'SNCF')
INSERT [dbo].[Companies] ([Id], [Title]) VALUES (8, N'Tokyo Metro Co., Ltd.')
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleTypes] ON 

INSERT [dbo].[VehicleTypes] ([Id], [Title]) VALUES (3, N'Airplane')
INSERT [dbo].[VehicleTypes] ([Id], [Title]) VALUES (1, N'Bus')
INSERT [dbo].[VehicleTypes] ([Id], [Title]) VALUES (5, N'Metro')
INSERT [dbo].[VehicleTypes] ([Id], [Title]) VALUES (4, N'Ship')
INSERT [dbo].[VehicleTypes] ([Id], [Title]) VALUES (2, N'Train')
SET IDENTITY_INSERT [dbo].[VehicleTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (1, N'Volvo 9700', 1, 50, N'BUS-1203')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (2, N'Scania Touring', 1, 52, N'BUS-1892')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (3, N'TGV Duplex', 2, 510, N'TRAIN-4411')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (4, N'Shinkansen N700', 2, 1323, N'TRAIN-8820')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (5, N'Boeing 777', 3, 396, N'AIR-777-IR')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (6, N'Airbus A380', 3, 469, N'AIR-A380-UK')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (7, N'Symphony of Seas', 4, 6680, N'SHIP-7788')
INSERT [dbo].[Vehicles] ([Id], [Title], [VehicleTypeId], [Capacity], [PlateNumber]) VALUES (8, N'Tokyo Metro 1000', 5, 144, N'METRO-1000')
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET IDENTITY_INSERT [dbo].[Transportations] ON 

INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (1, 4, 1, 1, 1, CAST(N'2025-04-07T07:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T08:30:00.0000000' AS DateTime2), N'TRP-10001', 50, CAST(55.00 AS Decimal(18, 2)), CAST(412.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (2, 5, 4, 7, 7, CAST(N'2025-04-07T13:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T15:00:00.0000000' AS DateTime2), N'TRP-10002', 6680, CAST(226.00 AS Decimal(18, 2)), CAST(416.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (3, 4, 1, 6, 6, CAST(N'2025-04-07T06:15:00.0000000' AS DateTime2), CAST(N'2025-04-07T10:15:00.0000000' AS DateTime2), N'TRP-10003', 469, CAST(45.00 AS Decimal(18, 2)), CAST(581.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (4, 4, 1, 1, 1, CAST(N'2025-04-07T03:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T10:30:00.0000000' AS DateTime2), N'TRP-10004', 50, CAST(41.00 AS Decimal(18, 2)), CAST(321.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (5, 5, 1, 4, 4, CAST(N'2025-04-07T06:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T10:00:00.0000000' AS DateTime2), N'TRP-10005', 1323, CAST(46.00 AS Decimal(18, 2)), CAST(379.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (6, 4, 5, 2, 2, CAST(N'2025-04-07T17:45:00.0000000' AS DateTime2), CAST(N'2025-04-08T01:45:00.0000000' AS DateTime2), N'TRP-10006', 52, CAST(71.00 AS Decimal(18, 2)), CAST(570.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (7, 3, 1, 3, 3, CAST(N'2025-04-07T02:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T07:30:00.0000000' AS DateTime2), N'TRP-10007', 510, CAST(251.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (8, 3, 5, 1, 1, CAST(N'2025-04-07T08:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T14:00:00.0000000' AS DateTime2), N'TRP-10008', 50, CAST(257.00 AS Decimal(18, 2)), CAST(432.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (9, 6, 4, 8, 8, CAST(N'2025-04-07T16:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T20:00:00.0000000' AS DateTime2), N'TRP-10009', 144, CAST(289.00 AS Decimal(18, 2)), CAST(462.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (10, 8, 2, 2, 2, CAST(N'2025-04-07T08:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T13:00:00.0000000' AS DateTime2), N'TRP-10010', 52, CAST(212.00 AS Decimal(18, 2)), CAST(516.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (11, 5, 4, 7, 7, CAST(N'2025-04-07T20:30:00.0000000' AS DateTime2), CAST(N'2025-04-08T00:30:00.0000000' AS DateTime2), N'TRP-10011', 6680, CAST(119.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (12, 5, 8, 3, 3, CAST(N'2025-04-07T05:45:00.0000000' AS DateTime2), CAST(N'2025-04-07T08:45:00.0000000' AS DateTime2), N'TRP-10012', 510, CAST(56.00 AS Decimal(18, 2)), CAST(436.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (13, 7, 8, 8, 8, CAST(N'2025-04-07T10:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T13:30:00.0000000' AS DateTime2), N'TRP-10013', 144, CAST(254.00 AS Decimal(18, 2)), CAST(381.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (14, 3, 5, 6, 6, CAST(N'2025-04-07T09:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T16:00:00.0000000' AS DateTime2), N'TRP-10014', 469, CAST(288.00 AS Decimal(18, 2)), CAST(451.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (15, 1, 2, 6, 6, CAST(N'2025-04-07T11:15:00.0000000' AS DateTime2), CAST(N'2025-04-07T15:15:00.0000000' AS DateTime2), N'TRP-10015', 469, CAST(127.00 AS Decimal(18, 2)), CAST(503.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (16, 1, 2, 1, 1, CAST(N'2025-04-07T08:15:00.0000000' AS DateTime2), CAST(N'2025-04-07T14:15:00.0000000' AS DateTime2), N'TRP-10016', 50, CAST(228.00 AS Decimal(18, 2)), CAST(584.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (17, 7, 1, 2, 2, CAST(N'2025-04-07T17:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T18:30:00.0000000' AS DateTime2), N'TRP-10017', 52, CAST(103.00 AS Decimal(18, 2)), CAST(368.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (18, 7, 6, 2, 2, CAST(N'2025-04-07T16:30:00.0000000' AS DateTime2), CAST(N'2025-04-07T17:30:00.0000000' AS DateTime2), N'TRP-10018', 52, CAST(124.00 AS Decimal(18, 2)), CAST(564.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (19, 6, 7, 1, 1, CAST(N'2025-04-07T03:00:00.0000000' AS DateTime2), CAST(N'2025-04-07T10:00:00.0000000' AS DateTime2), N'TRP-10019', 50, CAST(296.00 AS Decimal(18, 2)), CAST(480.00 AS Decimal(18, 2)))
INSERT [dbo].[Transportations] ([Id], [FromLocationId], [ToLocationId], [CompanyId], [VehicleId], [StartDateTime], [EndDateTime], [SerialNumber], [RemainingCapacity], [BasePrice], [VIPPrice]) VALUES (20, 8, 3, 1, 1, CAST(N'2025-04-07T20:00:00.0000000' AS DateTime2), CAST(N'2025-04-08T00:00:00.0000000' AS DateTime2), N'TRP-10020', 50, CAST(60.00 AS Decimal(18, 2)), CAST(519.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Transportations] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250317134753_InitialCreate', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327155709_RecreateDatabaseAfterFixes', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250406151909_AdjustTransportationEndDateTime', N'9.0.3')
GO
