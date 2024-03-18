USE [ECommerce]
GO
/****** Object:  Table [dbo].[Basket]    Script Date: 2024/03/18 20:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Basket](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2024/03/18 20:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2024/03/18 20:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CountInStock] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBaskets]    Script Date: 2024/03/18 20:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBaskets](
	[ProductId] [bigint] NOT NULL,
	[BasketId] [bigint] NOT NULL,
	[Count] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductBaskets] PRIMARY KEY CLUSTERED 
(
	[BasketId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Basket] ADD  DEFAULT ((0.0)) FOR [TotalPrice]
GO
ALTER TABLE [dbo].[Basket] ADD  DEFAULT ('2024-03-18T09:48:58.8548405+02:00') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ('2024-03-18T09:48:58.8548393+02:00') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [CountInStock]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('2024-03-18T09:48:58.8548378+02:00') FOR [DateCreated]
GO
ALTER TABLE [dbo].[ProductBaskets] ADD  DEFAULT ((1)) FOR [Count]
GO
ALTER TABLE [dbo].[ProductBaskets] ADD  DEFAULT ('2024-03-18T09:48:58.8545322+02:00') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Basket]  WITH CHECK ADD  CONSTRAINT [FK_Basket_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Basket] CHECK CONSTRAINT [FK_Basket_Customer_CustomerId]
GO
ALTER TABLE [dbo].[ProductBaskets]  WITH CHECK ADD  CONSTRAINT [FK_ProductBaskets_Basket_BasketId] FOREIGN KEY([BasketId])
REFERENCES [dbo].[Basket] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductBaskets] CHECK CONSTRAINT [FK_ProductBaskets_Basket_BasketId]
GO
ALTER TABLE [dbo].[ProductBaskets]  WITH CHECK ADD  CONSTRAINT [FK_ProductBaskets_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductBaskets] CHECK CONSTRAINT [FK_ProductBaskets_Product_ProductId]
GO
/****** Object:  StoredProcedure [dbo].[SearchProducts]    Script Date: 2024/03/18 20:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchProducts]
     @PreviousMonthsCount INT
AS
BEGIN 
  DECLARE @SearchDate DATETIME = DATEADD(MONTH, -@PreviousMonthsCount, GETDATE());

	SELECT TOP 10 p.Id, p.Name , SUM([Count])  'Purchased Count' , COUNT(c.Id) 'Unique Customers' FROM dbo.Product p
	INNER JOIN dbo.ProductBaskets pb ON p.Id = pb.ProductId
	INNER JOIN dbo.Basket bsk on bsk.Id = pb.BasketId
	INNER JOIN dbo.Customer c on c.id = bsk.CustomerId
	where pb.DateCreated >= @SearchDate
	Group By p.Name , pb.DateCreated ,p.Id
	Order BY pb.DateCreated asc
END
GO
