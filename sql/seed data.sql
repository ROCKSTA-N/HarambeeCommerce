USE [ECommerce]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [DateCreated]) VALUES (1, N'Rocky', N'Manganye', CAST(N'2024-03-18T09:48:58.8548393' AS DateTime2))
GO
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [DateCreated]) VALUES (2, N'John', N'Doe', CAST(N'2024-03-18T13:28:08.5920000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Basket] ON 
GO
INSERT [dbo].[Basket] ([Id], [CustomerId], [TotalPrice], [DateCreated]) VALUES (1, 1, CAST(1550.00 AS Decimal(18, 2)), CAST(N'2024-03-18T09:48:58.8548405' AS DateTime2))
GO
INSERT [dbo].[Basket] ([Id], [CustomerId], [TotalPrice], [DateCreated]) VALUES (2, 2, CAST(10.00 AS Decimal(18, 2)), CAST(N'2024-03-18T09:48:58.8548405' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Basket] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [CountInStock], [DateCreated]) VALUES (1, N'Book', N'Story book', CAST(10.00 AS Decimal(18, 2)), 12, CAST(N'2024-03-18T09:48:58.8548378' AS DateTime2))
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [CountInStock], [DateCreated]) VALUES (2, N'shoe', N'Nike shoe', CAST(500.00 AS Decimal(18, 2)), 500, CAST(N'2024-03-18T09:48:58.8548378' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[ProductBaskets] ([ProductId], [BasketId], [Count], [DateCreated]) VALUES (1, 1, 4, CAST(N'2024-03-18T09:48:58.8545322' AS DateTime2))
GO
INSERT [dbo].[ProductBaskets] ([ProductId], [BasketId], [Count], [DateCreated]) VALUES (2, 1, 2, CAST(N'2024-03-18T09:48:58.8545322' AS DateTime2))
GO
INSERT [dbo].[ProductBaskets] ([ProductId], [BasketId], [Count], [DateCreated]) VALUES (1, 2, 1, CAST(N'2024-03-18T09:48:58.8545322' AS DateTime2))
GO
