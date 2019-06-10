USE [NutritionalStuff]
GO
/****** Object:  Table [dbo].[FoodLogs]    Script Date: 10-6-2019 20:21:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodLogs](
	[FoodLogId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Amount] [int] NOT NULL,
	[UnitId] [tinyint] NOT NULL,
	[MealId] [tinyint] NOT NULL,
	[ConsumedEnergy] [decimal](9, 2) NOT NULL,
	[ConsumedFats] [decimal](9, 2) NOT NULL,
	[ConsumedSaturatedFats] [decimal](9, 2) NOT NULL,
	[ConsumedMonoUnsaturatedFats] [decimal](9, 2) NOT NULL,
	[ConsumedPolyUnsaturatedFats] [decimal](9, 2) NOT NULL,
	[ConsumedCarbohydrates] [decimal](9, 2) NOT NULL,
	[ConsumedSugar] [decimal](9, 2) NOT NULL,
	[ConsumedProteins] [decimal](9, 2) NOT NULL,
	[ConsumedSalt] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_FoodLogs] PRIMARY KEY CLUSTERED 
(
	[FoodLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[IngredientId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meals]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meals](
	[MealId] [tinyint] NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Meals] PRIMARY KEY CLUSTERED 
(
	[MealId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producers]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producers](
	[ProducerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Producers] PRIMARY KEY CLUSTERED 
(
	[ProducerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProducerId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[ValuesPer] [int] NOT NULL,
	[UnitId] [tinyint] NOT NULL,
	[Energy] [decimal](9, 2) NOT NULL,
	[Fats] [decimal](9, 2) NOT NULL,
	[SaturatedFats] [decimal](9, 2) NOT NULL,
	[MonoUnsaturatedFats] [decimal](9, 2) NOT NULL,
	[PolyUnsaturatedFats] [decimal](9, 2) NOT NULL,
	[Carbohydrates] [decimal](9, 2) NOT NULL,
	[Sugar] [decimal](9, 2) NOT NULL,
	[Proteins] [decimal](9, 2) NOT NULL,
	[Salt] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Servings] [int] NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 10-6-2019 20:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [tinyint] NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FoodLogs]  WITH CHECK ADD  CONSTRAINT [FK_FoodLogs_Meals] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meals] ([MealId])
GO
ALTER TABLE [dbo].[FoodLogs] CHECK CONSTRAINT [FK_FoodLogs_Meals]
GO
ALTER TABLE [dbo].[FoodLogs]  WITH CHECK ADD  CONSTRAINT [FK_FoodLogs_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[FoodLogs] CHECK CONSTRAINT [FK_FoodLogs_Products]
GO
ALTER TABLE [dbo].[FoodLogs]  WITH CHECK ADD  CONSTRAINT [FK_FoodLogs_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([UnitId])
GO
ALTER TABLE [dbo].[FoodLogs] CHECK CONSTRAINT [FK_FoodLogs_Units]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_Products]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_Recipes] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_Recipes]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Producers] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producers] ([ProducerId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Producers]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([UnitId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Units]
GO
