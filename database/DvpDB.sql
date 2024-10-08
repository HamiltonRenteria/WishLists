USE [DvpDB]
GO
ALTER TABLE [dbo].[Wishlist] DROP CONSTRAINT [FK_Wishlist_User]
GO
ALTER TABLE [dbo].[Wishlist] DROP CONSTRAINT [FK_Wishlist_Product]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category]
GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 25/09/2024 10:00:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wishlist]') AND type in (N'U'))
DROP TABLE [dbo].[Wishlist]
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/09/2024 10:00:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25/09/2024 10:00:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 25/09/2024 10:00:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type in (N'U'))
DROP TABLE [dbo].[Category]
GO
USE [master]
GO
/****** Object:  Database [DvpDB]    Script Date: 25/09/2024 10:00:38 ******/
DROP DATABASE [DvpDB]
GO
/****** Object:  Database [DvpDB]    Script Date: 25/09/2024 10:00:38 ******/
CREATE DATABASE [DvpDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DvpDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLDEV\MSSQL\DATA\DvpDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DvpDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLDEV\MSSQL\DATA\DvpDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DvpDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DvpDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DvpDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DvpDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DvpDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DvpDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DvpDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DvpDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DvpDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DvpDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DvpDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DvpDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DvpDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DvpDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DvpDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DvpDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DvpDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DvpDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DvpDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DvpDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DvpDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DvpDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DvpDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DvpDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DvpDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DvpDB] SET  MULTI_USER 
GO
ALTER DATABASE [DvpDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DvpDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DvpDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DvpDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DvpDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DvpDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DvpDB', N'ON'
GO
ALTER DATABASE [DvpDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [DvpDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DvpDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 25/09/2024 10:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25/09/2024 10:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Category] [nvarchar](200) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/09/2024 10:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 25/09/2024 10:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Wishlist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_Product]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_User]
GO
USE [master]
GO
ALTER DATABASE [DvpDB] SET  READ_WRITE 
GO
