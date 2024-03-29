USE [master]
GO
/****** Object:  Database [CoffeeShop]    Script Date: 9/29/2019 2:27:32 AM ******/
CREATE DATABASE [CoffeeShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoffeeShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SILENTREVENGER\MSSQL\DATA\CoffeeShop.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CoffeeShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SILENTREVENGER\MSSQL\DATA\CoffeeShop_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CoffeeShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoffeeShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoffeeShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoffeeShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeeShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoffeeShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoffeeShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoffeeShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoffeeShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoffeeShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoffeeShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoffeeShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoffeeShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoffeeShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoffeeShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoffeeShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoffeeShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoffeeShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CoffeeShop] SET  MULTI_USER 
GO
ALTER DATABASE [CoffeeShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoffeeShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoffeeShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoffeeShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CoffeeShop]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 9/29/2019 2:27:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[Contact] [varchar](20) NULL,
	[Address] [varchar](40) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/29/2019 2:27:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[Price] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 9/29/2019 2:27:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[OrderInformations]    Script Date: 9/29/2019 2:27:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OrderInformations] AS
SELECT Orders.Id, Customers.Name AS CustomerName, Items.Name AS ItemName, Orders.Quantity, Orders.TotalPrice FROM Orders,Customers,Items WHERE Customers.Id = Orders.CustomerId AND Items.Id = Orders.ItemId
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name], [Contact], [Address]) VALUES (1, N'Zahid', N'01775325698', N'mirpur')
INSERT [dbo].[Customers] ([Id], [Name], [Contact], [Address]) VALUES (2, N'Masud', N'01685123654', N'Farmgate')
INSERT [dbo].[Customers] ([Id], [Name], [Contact], [Address]) VALUES (3, N'Murad', N'01927145236', N'Gulshan')
INSERT [dbo].[Customers] ([Id], [Name], [Contact], [Address]) VALUES (4, N'Hasan', N'01775748596', N'shewrapara')
INSERT [dbo].[Customers] ([Id], [Name], [Contact], [Address]) VALUES (5, N'Milon', N'01654789852', N'shewrapara')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (1, N'Black', 120)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (2, N'Cold', 100)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (3, N'Hot', 95)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (4, N'Regular', 80)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (5, N'Indian Coffee', 135)
SET IDENTITY_INSERT [dbo].[Items] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [CustomerId], [ItemId], [Quantity], [TotalPrice]) VALUES (1, 1, 3, 4, 380)
INSERT [dbo].[Orders] ([Id], [CustomerId], [ItemId], [Quantity], [TotalPrice]) VALUES (2, 4, 2, 5, 500)
SET IDENTITY_INSERT [dbo].[Orders] OFF
USE [master]
GO
ALTER DATABASE [CoffeeShop] SET  READ_WRITE 
GO
