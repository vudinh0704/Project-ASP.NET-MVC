USE [master]
GO
/****** Object:  Database [NexusDB]    Script Date: 30/09/2020 1:52:56 PM ******/
CREATE DATABASE [NexusDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NexusDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.VUDINH\MSSQL\DATA\NexusDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NexusDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.VUDINH\MSSQL\DATA\NexusDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NexusDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NexusDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NexusDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NexusDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NexusDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NexusDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NexusDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NexusDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NexusDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NexusDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NexusDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NexusDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NexusDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NexusDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NexusDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NexusDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NexusDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NexusDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NexusDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NexusDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NexusDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NexusDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NexusDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NexusDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NexusDB] SET RECOVERY FULL 
GO
ALTER DATABASE [NexusDB] SET  MULTI_USER 
GO
ALTER DATABASE [NexusDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NexusDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NexusDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NexusDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NexusDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NexusDB', N'ON'
GO
ALTER DATABASE [NexusDB] SET QUERY_STORE = OFF
GO
USE [NexusDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [NexusDB]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 30/09/2020 1:52:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[account_id] [nvarchar](255) NOT NULL,
	[account_retailShowroom_id] [int] NULL,
	[account_name] [nvarchar](255) NULL,
	[account_password] [nvarchar](255) NULL,
	[account_role] [nvarchar](255) NULL,
	[account_phone] [nvarchar](255) NULL,
	[account_email] [nvarchar](255) NULL,
	[account_address] [nvarchar](255) NULL,
	[account_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bills]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bills](
	[bill_id] [varchar](20) NOT NULL,
	[bill_order_id] [varchar](20) NULL,
	[bill_track_id] [varchar](50) NULL,
	[bill_date] [datetime] NULL,
	[bill_status] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[city_id] [varchar](20) NOT NULL,
	[city_name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[connections]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[connections](
	[connection_id] [int] IDENTITY(1,1) NOT NULL,
	[connection_name] [nvarchar](255) NULL,
	[connection_description] [text] NULL,
	[connection_image_thumbnail] [nvarchar](255) NULL,
	[connection_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[connection_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipments]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipments](
	[equipment_id] [int] IDENTITY(1,1) NOT NULL,
	[equipment_vendor_id] [int] NULL,
	[equipment_name] [nvarchar](255) NULL,
	[equipment_type] [nvarchar](255) NULL,
	[equipment_image_thumbnail] [nvarchar](255) NULL,
	[equipment_content] [text] NULL,
	[equipment_price] [float] NULL,
	[equipment_quantity] [int] NULL,
	[equipment_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[equipment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipments_orders]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipments_orders](
	[equipments_orders_id] [int] IDENTITY(1,1) NOT NULL,
	[equipments_orders_equipment_id] [int] NULL,
	[equipments_orders_order_id] [varchar](20) NULL,
	[equipments_orders_quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[equipments_orders_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedbacks]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedbacks](
	[feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[feedback_account_id] [nvarchar](255) NULL,
	[feedback_equipment_id] [int] NULL,
	[feedback_plan_id] [int] NULL,
	[feedback_title] [nvarchar](255) NULL,
	[feedback_content] [text] NULL,
	[feedback_created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [varchar](20) NOT NULL,
	[order_account_id] [nvarchar](255) NULL,
	[order_retailShowroom_id] [int] NULL,
	[order_status] [nvarchar](255) NULL,
	[order_feasibility] [nvarchar](255) NULL,
	[order_phone] [nvarchar](255) NULL,
	[order_address] [nvarchar](255) NULL,
	[order_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plans]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plans](
	[plan_id] [int] IDENTITY(1,1) NOT NULL,
	[plan_connection_id] [int] NULL,
	[plan_name] [nvarchar](255) NULL,
	[plan_description] [text] NULL,
	[plan_image_thumbnail] [nvarchar](255) NULL,
	[plan_fixed_price] [float] NULL,
	[plan_local_price] [float] NULL,
	[plan_std_price] [float] NULL,
	[plan_messaging_for_mobiles_price] [float] NULL,
	[plan_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[plan_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plans_orders]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plans_orders](
	[plans_orders_id] [varchar](15) NOT NULL,
	[plans_orders_plan_id] [int] NULL,
	[plans_orders_order_id] [varchar](20) NULL,
	[plans_orders_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[plans_orders_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replies]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replies](
	[reply_id] [int] IDENTITY(1,1) NOT NULL,
	[reply_account_id] [nvarchar](255) NULL,
	[reply_feedback_id] [int] NULL,
	[reply_content] [text] NULL,
	[reply_created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[reply_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[retailShowrooms]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[retailShowrooms](
	[retailShowroom_id] [int] IDENTITY(1,1) NOT NULL,
	[retailShowroom_city_id] [varchar](20) NULL,
	[retailShowroom_name] [nvarchar](255) NULL,
	[retailShowroom_address] [nvarchar](255) NULL,
	[retailShowroom_status] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[retailShowroom_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tracks]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tracks](
	[track_id] [varchar](50) NOT NULL,
	[track_plans_orders_id] [varchar](15) NULL,
	[track_date_from] [varchar](20) NULL,
	[track_date_to] [varchar](20) NULL,
	[track_time_used_local] [int] NULL,
	[track_time_used_std] [int] NULL,
	[track_time_used_msg] [int] NULL,
	[track_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[track_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vendors]    Script Date: 30/09/2020 1:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vendors](
	[vendor_id] [int] IDENTITY(1,1) NOT NULL,
	[vendor_name] [nvarchar](255) NULL,
	[vendor_address] [nvarchar](255) NULL,
	[vendor_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[vendor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'accountant', 1, N'Kelly', N'698d51a19d8a121ce581499d7b701668', N'Accountant', N'0909223344', N'accountant@gmail.com', N'Accountant street, Venice', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'admin', 1, N'Simon', N'698d51a19d8a121ce581499d7b701668', N'Admin', N'0909112233', N'admin@gmail.com', N'Admin street, Rio de Janeiro', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'D111D0000000003', NULL, NULL, N'5f4dcc3b5aa765d61d8327deb882cf99', N'Customer', N'0404040404', N'default@gmail.com', N'911 PAZ street, New York', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'salesperson01', 1, N'May', N'698d51a19d8a121ce581499d7b701668', N'SalesPerson', N'0909445566', N'salesperson01@gmail.com', N'Sales Person 01 street, New York', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'salesperson02', 2, N'Jack', N'698d51a19d8a121ce581499d7b701668', N'SalesPerson', N'0909556677', N'salesperson02@gmail.com', N'Sales Person 02 street, Paris', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'salesperson03', 3, N'Scarlet', N'698d51a19d8a121ce581499d7b701668', N'SalesPerson', N'0909667788', N'salesperson03@gmail.com', N'Sales Person 03 street, Hiroshima', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'salesperson04', 4, N'Michelle', N'698d51a19d8a121ce581499d7b701668', N'SalesPerson', N'0909778899', N'salesperson04@gmail.com', N'Sales Person 04 street, Tokyo', N'Active')
INSERT [dbo].[accounts] ([account_id], [account_retailShowroom_id], [account_name], [account_password], [account_role], [account_phone], [account_email], [account_address], [account_status]) VALUES (N'technician', 4, N'David', N'698d51a19d8a121ce581499d7b701668', N'Technician', N'0909334455', N'technician@gmail.com', N'Technician street, Luxembourg', N'Active')
INSERT [dbo].[bills] ([bill_id], [bill_order_id], [bill_track_id], [bill_date], [bill_status]) VALUES (N'0000000001', NULL, N'T0000000001-01-01-2020', CAST(N'2020-09-24T06:50:59.097' AS DateTime), N'Unpaid')
INSERT [dbo].[bills] ([bill_id], [bill_order_id], [bill_track_id], [bill_date], [bill_status]) VALUES (N'0000000002', N'D0000000003', NULL, CAST(N'2020-09-24T06:51:16.513' AS DateTime), N'Unpaid')
INSERT [dbo].[bills] ([bill_id], [bill_order_id], [bill_track_id], [bill_date], [bill_status]) VALUES (N'0000000003', NULL, N'T0000000003-01-01-2020', CAST(N'2020-09-24T06:53:49.190' AS DateTime), N'Unpaid')
INSERT [dbo].[bills] ([bill_id], [bill_order_id], [bill_track_id], [bill_date], [bill_status]) VALUES (N'0000000004', NULL, N'T0000000001-02-01-2020', CAST(N'2020-09-24T06:53:53.497' AS DateTime), N'Unpaid')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'111', N'New York')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'112', N'Paris')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'113', N'Hiroshima')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'114', N'Tokyo')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'115', N'Cairo')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'116', N'Copenhagen')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'117', N'Frankfurt')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'118', N'Hong Kong')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'119', N'Johannesburg')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'120', N'Kuala Lumpur')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'121', N'Los Angeles')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'122', N'Luxembourg')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'123', N'Madrid')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'124', N'Vienna')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'125', N'Venice')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'126', N'Toronto')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'127', N'Seoul')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'128', N'Sao Paulo')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'129', N'San Francisco')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'130', N'Rome')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'131', N'Stockholm')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'132', N'Sydney')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'133', N'Rio de Janeiro')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'134', N'Santiago')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'135', N'Reykjavik')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'136', N'Ottawa')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'137', N'Oslo')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'138', N'Mumbai')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'139', N'Montreal')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'140', N'Manila')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'141', N'Dubai')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'142', N'Geneva')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'143', N'Jakarta')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'144', N'London')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'145', N'Luxembourg')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'146', N'Wellington')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'147', N'The Hague')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'148', N'Budapest')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'149', N'Berlin')
INSERT [dbo].[cities] ([city_id], [city_name]) VALUES (N'150', N'Amsterdam')
SET IDENTITY_INSERT [dbo].[connections] ON 

INSERT [dbo].[connections] ([connection_id], [connection_name], [connection_description], [connection_image_thumbnail], [connection_status]) VALUES (1, N'Dial - Up Connection', N'...', N'dialup-connection.jpg', N'Active')
INSERT [dbo].[connections] ([connection_id], [connection_name], [connection_description], [connection_image_thumbnail], [connection_status]) VALUES (2, N'Land Line Connection', N'...', N'landline-connection.jpg', N'Active')
INSERT [dbo].[connections] ([connection_id], [connection_name], [connection_description], [connection_image_thumbnail], [connection_status]) VALUES (3, N'Broad Band Connection', N'...', N'broadband-connection.jpg', N'Active')
SET IDENTITY_INSERT [dbo].[connections] OFF
SET IDENTITY_INSERT [dbo].[equipments] ON 

INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (1, 2, N'Cable 1', N'Cable', N'equipment-7.jpg', N'...', 5.35, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (2, 2, N'Cable 2', N'Cable', N'equipment-7.jpg', N'...', 5.15, 462, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (3, 1, N'Cable 3', N'Cable', N'equipment-7.jpg', N'...', 9.98, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (4, 1, N'Cable 4', N'Cable', N'equipment-7.jpg', N'...', 4.26, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (5, 1, N'Cable 5', N'Cable', N'equipment-7.jpg', N'...', 9.89, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (6, 1, N'Modem 1', N'Modem', N'equipment-2.jpg', N'...', 50.15, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (7, 1, N'Modem 2', N'Modem', N'equipment-2.jpg', N'...', 49.89, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (8, 1, N'Modem 3', N'Modem', N'equipment-2.jpg', N'...', 60.25, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (9, 1, N'Modem 4', N'Modem', N'equipment-2.jpg', N'...', 98.02, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (10, 1, N'Modem 5', N'Modem', N'equipment-2.jpg', N'...', 95.97, 465, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (11, 2, N'Router 1', N'Router', N'equipment-1.jpg', N'...', 100.5, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (12, 2, N'Router 2', N'Router', N'equipment-1.jpg', N'...', 111.02, 498, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (13, 2, N'Router 3', N'Router', N'equipment-1.jpg', N'...', 99.89, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (14, 2, N'Router 4', N'Router', N'equipment-1.jpg', N'...', 101.9, 500, N'Active')
INSERT [dbo].[equipments] ([equipment_id], [equipment_vendor_id], [equipment_name], [equipment_type], [equipment_image_thumbnail], [equipment_content], [equipment_price], [equipment_quantity], [equipment_status]) VALUES (15, 2, N'Router 5', N'Router', N'equipment-1.jpg', N'...', 112.5, 500, N'Active')
SET IDENTITY_INSERT [dbo].[equipments] OFF
SET IDENTITY_INSERT [dbo].[equipments_orders] ON 

INSERT [dbo].[equipments_orders] ([equipments_orders_id], [equipments_orders_equipment_id], [equipments_orders_order_id], [equipments_orders_quantity]) VALUES (1, 2, N'T0000000001', 10)
INSERT [dbo].[equipments_orders] ([equipments_orders_id], [equipments_orders_equipment_id], [equipments_orders_order_id], [equipments_orders_quantity]) VALUES (2, 10, N'T0000000001', 10)
INSERT [dbo].[equipments_orders] ([equipments_orders_id], [equipments_orders_equipment_id], [equipments_orders_order_id], [equipments_orders_quantity]) VALUES (3, 10, N'T0000000003', 15)
INSERT [dbo].[equipments_orders] ([equipments_orders_id], [equipments_orders_equipment_id], [equipments_orders_order_id], [equipments_orders_quantity]) VALUES (4, 2, N'B0000000002', 5)
SET IDENTITY_INSERT [dbo].[equipments_orders] OFF
SET IDENTITY_INSERT [dbo].[feedbacks] ON 

INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (1, N'D111D0000000003', 1, NULL, N'Good service!', N'...', CAST(N'2020-09-24T22:15:42.127' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (2, N'D111D0000000003', 1, NULL, N'What''s this?', N'...', CAST(N'2020-09-24T22:27:37.267' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (3, N'D111D0000000003', NULL, 1, N'Good service!', N'...', CAST(N'2020-09-24T22:33:15.170' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (4, N'D111D0000000003', 1, NULL, N'Bad Service!!!', N'All your equipments are low quality.', CAST(N'2020-09-25T11:32:28.377' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (5, N'D111D0000000003', 1, NULL, N'It''s so cheap!', N'...', CAST(N'2020-09-25T12:14:24.867' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (6, N'D111D0000000003', 1, NULL, N'Without saying...', N'...', CAST(N'2020-09-25T12:32:41.980' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (7, N'D111D0000000003', 1, NULL, N'We don''t talk anymore...', N'...', CAST(N'2020-09-25T12:44:09.473' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (8, N'D111D0000000003', 1, NULL, N'No feeling...', N'...', CAST(N'2020-09-25T13:34:55.770' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (10, N'D111D0000000003', 2, NULL, N'Good service!', N'...', CAST(N'2020-09-25T13:39:44.137' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (13, N'D111D0000000003', 3, NULL, N'Good service!', N'...', CAST(N'2020-09-25T13:57:35.177' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (14, N'D111D0000000003', 3, NULL, N'Bad Service!!!', N'...', CAST(N'2020-09-25T13:58:25.807' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (15, N'D111D0000000003', 3, NULL, N'Bad Service!!!', N'...', CAST(N'2020-09-25T13:58:32.573' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (16, N'D111D0000000003', 5, NULL, N'Bad Service!!!', N'...', CAST(N'2020-09-25T14:00:33.517' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (17, N'D111D0000000003', NULL, 8, N'Good service!', N'...', CAST(N'2020-09-25T14:01:19.173' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (18, N'D111D0000000003', NULL, 8, N'Bad Service!!!', N'...', CAST(N'2020-09-25T14:03:37.837' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (19, N'D111D0000000003', NULL, 9, N'Bad Service!!!', N'...', CAST(N'2020-09-25T14:05:16.593' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (20, N'D111D0000000003', NULL, 12, N'No feeling...', N'...', CAST(N'2020-09-25T14:06:42.070' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (21, N'D111D0000000003', 11, NULL, N'Good service!', N'...', CAST(N'2020-09-25T14:10:34.537' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (22, N'D111D0000000003', NULL, 14, N'Good service!', N'...', CAST(N'2020-09-25T14:11:02.770' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (23, N'D111D0000000003', 7, NULL, N'Good service!', N'...', CAST(N'2020-09-25T18:36:03.747' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (24, N'D111D0000000003', NULL, 1, N'No feeling...', N'...', CAST(N'2020-09-25T20:05:50.933' AS DateTime))
INSERT [dbo].[feedbacks] ([feedback_id], [feedback_account_id], [feedback_equipment_id], [feedback_plan_id], [feedback_title], [feedback_content], [feedback_created_at]) VALUES (25, N'D111D0000000003', NULL, 1, N'Without saying...', N'...', CAST(N'2020-09-25T20:10:28.697' AS DateTime))
SET IDENTITY_INSERT [dbo].[feedbacks] OFF
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'B0000000001', NULL, 1, N'Pending', N'Pending', N'0505050505', N'819 HIP street, Cairo', CAST(N'2020-09-24T06:31:20.487' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'B0000000002', N'D111D0000000003', NULL, N'Pending', N'Pending', N'0404040404', N'911 PAZ street, New York', CAST(N'2020-09-28T00:52:02.233' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'D0000000001', NULL, NULL, N'Pending', N'Pending', N'0101010101', N'989 ZYQ street, Cairo', CAST(N'2020-09-24T05:24:00.140' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'D0000000002', NULL, 1, N'Pending', N'Feasible', N'0202020202', N'199 YOI street, Tokyo', CAST(N'2020-09-24T05:28:00.437' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'D0000000003', N'D111D0000000003', NULL, N'Billed', N'Feasible', N'0404040404', N'911 PAZ street, New York', CAST(N'2020-09-24T06:27:03.610' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'T0000000001', NULL, 1, N'Complete', N'Feasible', N'0303030303', N'999 RIK street, Seoul', CAST(N'2020-09-24T05:33:54.737' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'T0000000002', N'D111D0000000003', NULL, N'Pending', N'Pending', N'0404040404', N'921 RIO street, New York', CAST(N'2020-09-24T06:32:43.257' AS DateTime))
INSERT [dbo].[orders] ([order_id], [order_account_id], [order_retailShowroom_id], [order_status], [order_feasibility], [order_phone], [order_address], [order_date]) VALUES (N'T0000000003', N'D111D0000000003', 7, N'Complete', N'Feasible', N'0404040404', N'911 PAZ street, New York', CAST(N'2020-09-24T06:34:26.330' AS DateTime))
SET IDENTITY_INSERT [dbo].[plans] ON 

INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (1, 1, N'Dial - Up: Hourly Basis - Monthly', N'10 Hrs (valid for 1 month)', N'plan-9.jpg', 50, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (2, 1, N'Dial - Up: Hourly Basis - Quarterly', N'30 Hrs (valid for 3 months)', N'plan-9.jpg', 130, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (3, 1, N'Dial - Up: Hourly Basis - Half-Yearly', N'60 Hrs (valid for 6 months)', N'plan-9.jpg', 260, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (4, 1, N'Dial - Up: Unlimited 28Kbps - Monthly', N'Unlimited 28Kbps, valid for 1 month', N'plan-9.jpg', 75, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (5, 1, N'Dial - Up: Unlimited 28Kbps - Quarterly', N'Unlimited 28Kbps, valid for 3 months', N'plan-9.jpg', 150, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (6, 1, N'Dial - Up: Unlimited 56Kbps - Monthly', N'Unlimited 56Kbps, valid for 1 month', N'plan-9.jpg', 100, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (7, 1, N'Dial - Up: Unlimited 56Kbps - Quarterly', N'Unlimited 56Kbps, valid for 3 months', N'plan-9.jpg', 180, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (8, 2, N'Land Line: Local Plan - Unlimited - Monthly', N'Unlimited, valid for 1 month and this is retal', N'plan-9.jpg', 35, 0.75, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (9, 2, N'Land Line: Local Plan - Unlimited - Yearly', N'Unlimited, valid for 12 months and this is retal', N'plan-9.jpg', 75, 0.55, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (10, 2, N'Land Line: STD Plan - Monthly', N'Valid for 1 month and this is retal', N'plan-9.jpg', 125, 0.7, 2.25, 1, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (11, 2, N'Land Line: STD Plan - Half-Yearly', N'Valid for 6 months and this is retal', N'plan-9.jpg', 420, 0.6, 2, 1.15, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (12, 2, N'Land Line: STD Plan - Yearly', N'Valid for 12 months and this is retal', N'plan-9.jpg', 800, 0.6, 1.75, 1.25, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (13, 3, N'Broad Band: Hourly Basis - Monthly', N'30 Hrs (valid for 1 month)', N'plan-9.jpg', 175, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (14, 3, N'Broad Band: Hourly Basis - Half-Yearly', N'60 Hrs (valid for 6 months)', N'plan-9.jpg', 315, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (15, 3, N'Broad Band: Unlimited 64Kbps - Monthly', N'Unlimited 64Kbps, valid for 1 month', N'plan-9.jpg', 225, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (16, 3, N'Broad Band: Unlimited 64Kbps - Quarterly', N'Unlimited 64Kbps, valid for 3 months', N'plan-9.jpg', 400, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (17, 3, N'Broad Band: Unlimited 128Kbps - Monthly', N'Unlimited 128Kbps, valid for 1 month', N'plan-9.jpg', 350, 0, 0, 0, NULL)
INSERT [dbo].[plans] ([plan_id], [plan_connection_id], [plan_name], [plan_description], [plan_image_thumbnail], [plan_fixed_price], [plan_local_price], [plan_std_price], [plan_messaging_for_mobiles_price], [plan_status]) VALUES (18, 3, N'Broad Band: Unlimited 128Kbps - Quarterly', N'Unlimited 128Kbps, valid for 3 months', N'plan-9.jpg', 445, 0, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[plans] OFF
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'B0000000001-01', 15, N'B0000000001', N'Pending')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'B0000000002-01', 17, N'B0000000002', N'Pending')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'D0000000001-01', 2, N'D0000000001', N'Pending')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'D0000000002-01', 2, N'D0000000002', N'Pending')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'D0000000003-01', 2, N'D0000000003', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'D0000000003-02', 14, N'D0000000003', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000001-01', 9, N'T0000000001', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000001-02', 9, N'T0000000001', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000001-03', 14, N'T0000000001', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000001-04', 7, N'T0000000001', N'Connected')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000002-01', 12, N'T0000000002', N'Pending')
INSERT [dbo].[plans_orders] ([plans_orders_id], [plans_orders_plan_id], [plans_orders_order_id], [plans_orders_status]) VALUES (N'T0000000003-01', 11, N'T0000000003', N'Connected')
SET IDENTITY_INSERT [dbo].[replies] ON 

INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (5, N'D111D0000000003', 1, N'...', CAST(N'2020-09-25T11:04:35.153' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (6, N'D111D0000000003', 1, N'...', CAST(N'2020-09-25T11:09:21.120' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (7, N'D111D0000000003', 1, N'...', CAST(N'2020-09-25T11:57:06.157' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (8, N'D111D0000000003', 1, N'123...', CAST(N'2020-09-25T12:07:03.770' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (9, N'D111D0000000003', 1, N'234...', CAST(N'2020-09-25T12:09:46.650' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (10, N'D111D0000000003', 1, N'345...', CAST(N'2020-09-25T12:11:19.017' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (11, N'D111D0000000003', 1, N'456...', CAST(N'2020-09-25T12:14:53.273' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (12, N'D111D0000000003', 1, N'567...', CAST(N'2020-09-25T12:16:27.767' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (13, N'D111D0000000003', 1, N'678...', CAST(N'2020-09-25T12:24:49.067' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (14, N'D111D0000000003', 1, N'789...', CAST(N'2020-09-25T12:27:21.503' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (15, N'D111D0000000003', 1, N'891...', CAST(N'2020-09-25T12:32:04.710' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (16, N'D111D0000000003', 7, N'Ok...', CAST(N'2020-09-25T12:44:41.533' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (17, N'D111D0000000003', 7, N'Why?!', CAST(N'2020-09-25T12:45:21.500' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (18, N'D111D0000000003', 6, N'Why?', CAST(N'2020-09-25T12:45:41.277' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (19, N'D111D0000000003', 6, N'''Cause I like it!', CAST(N'2020-09-25T12:46:02.240' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (20, N'D111D0000000003', 2, N'This is a cable...', CAST(N'2020-09-25T13:32:23.897' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (21, N'D111D0000000003', 16, N'Why?!', CAST(N'2020-09-25T14:00:44.443' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (22, N'D111D0000000003', 21, N'Thank you!', CAST(N'2020-09-25T14:10:44.543' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (23, N'D111D0000000003', 22, N'Thank you!', CAST(N'2020-09-25T14:11:09.827' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (24, N'D111D0000000003', 23, N'Thank you!', CAST(N'2020-09-25T18:36:24.937' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (25, N'D111D0000000003', 24, N'Why?!', CAST(N'2020-09-25T20:05:59.720' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (26, N'D111D0000000003', 24, N'No reason.', CAST(N'2020-09-25T20:06:11.667' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (27, N'D111D0000000003', 24, N'Tell me why...', CAST(N'2020-09-25T20:06:51.743' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (28, N'D111D0000000003', 24, N'No, I can''t.', CAST(N'2020-09-25T20:09:39.530' AS DateTime))
INSERT [dbo].[replies] ([reply_id], [reply_account_id], [reply_feedback_id], [reply_content], [reply_created_at]) VALUES (29, N'D111D0000000003', 3, N'Thank you!', CAST(N'2020-09-25T20:09:52.440' AS DateTime))
SET IDENTITY_INSERT [dbo].[replies] OFF
SET IDENTITY_INSERT [dbo].[retailShowrooms] ON 

INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (1, N'111', N'Retail Showroom 1', N'1899 LIZ street, New York', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (2, N'112', N'Retail Showroom 2', N'250 KIO street, Paris', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (3, N'113', N'Retail Showroom 3', N'270 TUY street, Hiroshima', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (4, N'114', N'Retail Showroom 4', N'899 ZVY street, Tokyo', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (5, N'115', N'Retail Showroom 5', N'1052 BMW street, Cairo', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (6, N'116', N'Retail Showroom 6', N'899 ERT street, Copenhagen', N'Active')
INSERT [dbo].[retailShowrooms] ([retailShowroom_id], [retailShowroom_city_id], [retailShowroom_name], [retailShowroom_address], [retailShowroom_status]) VALUES (7, N'118', N'Retail Showroom 7', N'1122 UIL street, Hong Kong', N'Active')
SET IDENTITY_INSERT [dbo].[retailShowrooms] OFF
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000001-01-01-2020', N'T0000000001-01', N'01/01/2020', N'01/31/2020', 119, 0, 0, N'Billed')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000001-01-02-2020', N'T0000000001-01', N'02/01/2020', N'02/29/2020', 115, 0, 0, N'Complete')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000001-01-04-2020', N'T0000000001-01', N'04/01/2020', N'04/30/2020', 989, 0, 0, N'Complete')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000001-02-01-2020', N'T0000000001-02', N'01/01/2020', N'01/31/2020', 98, 0, 0, N'Billed')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000001-02-02-2020', N'T0000000001-02', N'02/01/2020', N'02/29/2020', 256, 0, 0, N'Complete')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000003-01-01-2020', N'T0000000003-01', N'01/01/2020', N'01/31/2020', 99, 129, 129, N'Billed')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000003-01-02-2020', N'T0000000003-01', N'02/01/2020', N'02/29/2020', 198, 190, 190, N'Complete')
INSERT [dbo].[tracks] ([track_id], [track_plans_orders_id], [track_date_from], [track_date_to], [track_time_used_local], [track_time_used_std], [track_time_used_msg], [track_status]) VALUES (N'T0000000003-01-03-2020', N'T0000000003-01', N'03/01/2020', N'03/31/2020', 899, 989, 1009, N'Pending')
SET IDENTITY_INSERT [dbo].[vendors] ON 

INSERT [dbo].[vendors] ([vendor_id], [vendor_name], [vendor_address], [vendor_status]) VALUES (1, N'VNPT', N'1010 REK street, Venice', NULL)
INSERT [dbo].[vendors] ([vendor_id], [vendor_name], [vendor_address], [vendor_status]) VALUES (2, N'FPT', N'980 POI street, Seoul', NULL)
INSERT [dbo].[vendors] ([vendor_id], [vendor_name], [vendor_address], [vendor_status]) VALUES (3, N'AT&T', N'899 REW street, New York', NULL)
INSERT [dbo].[vendors] ([vendor_id], [vendor_name], [vendor_address], [vendor_status]) VALUES (4, N'Verizon', N'1599 KAR street, Wellington', NULL)
SET IDENTITY_INSERT [dbo].[vendors] OFF
ALTER TABLE [dbo].[accounts]  WITH CHECK ADD FOREIGN KEY([account_retailShowroom_id])
REFERENCES [dbo].[retailShowrooms] ([retailShowroom_id])
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD FOREIGN KEY([bill_order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD FOREIGN KEY([bill_track_id])
REFERENCES [dbo].[tracks] ([track_id])
GO
ALTER TABLE [dbo].[equipments]  WITH CHECK ADD FOREIGN KEY([equipment_vendor_id])
REFERENCES [dbo].[vendors] ([vendor_id])
GO
ALTER TABLE [dbo].[equipments_orders]  WITH CHECK ADD FOREIGN KEY([equipments_orders_equipment_id])
REFERENCES [dbo].[equipments] ([equipment_id])
GO
ALTER TABLE [dbo].[equipments_orders]  WITH CHECK ADD FOREIGN KEY([equipments_orders_order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[feedbacks]  WITH CHECK ADD FOREIGN KEY([feedback_account_id])
REFERENCES [dbo].[accounts] ([account_id])
GO
ALTER TABLE [dbo].[feedbacks]  WITH CHECK ADD FOREIGN KEY([feedback_equipment_id])
REFERENCES [dbo].[equipments] ([equipment_id])
GO
ALTER TABLE [dbo].[feedbacks]  WITH CHECK ADD FOREIGN KEY([feedback_plan_id])
REFERENCES [dbo].[plans] ([plan_id])
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([order_account_id])
REFERENCES [dbo].[accounts] ([account_id])
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([order_retailShowroom_id])
REFERENCES [dbo].[retailShowrooms] ([retailShowroom_id])
GO
ALTER TABLE [dbo].[plans]  WITH CHECK ADD FOREIGN KEY([plan_connection_id])
REFERENCES [dbo].[connections] ([connection_id])
GO
ALTER TABLE [dbo].[plans_orders]  WITH CHECK ADD FOREIGN KEY([plans_orders_plan_id])
REFERENCES [dbo].[plans] ([plan_id])
GO
ALTER TABLE [dbo].[plans_orders]  WITH CHECK ADD FOREIGN KEY([plans_orders_order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[replies]  WITH CHECK ADD FOREIGN KEY([reply_account_id])
REFERENCES [dbo].[accounts] ([account_id])
GO
ALTER TABLE [dbo].[replies]  WITH CHECK ADD FOREIGN KEY([reply_feedback_id])
REFERENCES [dbo].[feedbacks] ([feedback_id])
GO
ALTER TABLE [dbo].[retailShowrooms]  WITH CHECK ADD FOREIGN KEY([retailShowroom_city_id])
REFERENCES [dbo].[cities] ([city_id])
GO
ALTER TABLE [dbo].[tracks]  WITH CHECK ADD FOREIGN KEY([track_plans_orders_id])
REFERENCES [dbo].[plans_orders] ([plans_orders_id])
GO
USE [master]
GO
ALTER DATABASE [NexusDB] SET  READ_WRITE 
GO