USE [master]
GO

/****** Object:  Database [ARANDA.CATALOG_PRODUCT] ******/
CREATE DATABASE [ARANDA.CATALOG_PRODUCT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ARANDA.CATALOG_PRODUCT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ARANDA.CATALOG_PRODUCT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ARANDA.CATALOG_PRODUCT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ARANDA.CATALOG_PRODUCT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ARANDA.CATALOG_PRODUCT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ARITHABORT OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET  MULTI_USER 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ARANDA.CATALOG_PRODUCT] SET  READ_WRITE 
GO