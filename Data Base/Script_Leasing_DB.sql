USE [master]
GO
CREATE DATABASE [Leasing]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Leasing_data', FILENAME = N'D:\BSUIR\curs\BD\Leasing_data.mdf' , SIZE = 5120KB , MAXSIZE = 76800KB , FILEGROWTH = 3072KB ), 
 FILEGROUP [Secondary] 
( NAME = N'Leasing2_data', FILENAME = N'D:\BSUIR\curs\BD\Leasing_data2.ndf' , SIZE = 3072KB , MAXSIZE = 51200KB , FILEGROWTH = 15%),
( NAME = N'Leasing3_data', FILENAME = N'D:\BSUIR\curs\BD\Leasing_data3.ndf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 4096KB )
 LOG ON 
( NAME = N'Leasing_log', FILENAME = N'D:\BSUIR\curs\BD\Leasing_log.ldf' , SIZE = 1024KB , MAXSIZE = 10240KB , FILEGROWTH = 20%), 
( NAME = N'Leasing2_log', FILENAME = N'D:\BSUIR\curs\BD\Leasing_log2.ldf' , SIZE = 512KB , MAXSIZE = 15360KB , FILEGROWTH = 10%)
GO
USE [Leasing]
GO
CREATE TABLE [dbo].[Car](
	[VIN] [varchar](15) NOT NULL,
	[brand] [varchar](15) NULL,
	[name] [varchar](10) NULL,
	[gearbox] [varchar](15) NULL,
	[price_currency] [varchar](10) NULL,
	[price] [int] NULL,
	[description] [varchar](512) NULL,
	[max_speed] [int] NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED (
	[VIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Car] ADD  CONSTRAINT [DF_Car_gearbox]  DEFAULT ('Автоматическая') FOR [gearbox]
GO
ALTER TABLE [dbo].[Car] ADD  CONSTRAINT [DF_Car_price_currency]  DEFAULT ('$') FOR [price_currency]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [CK_Car_Price] CHECK  (([price]>(0)))
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [CK_Car_Price]
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
[password] [varchar](50) NOT NULL,
	[surname] [varchar](15) NULL,
	[name] [varchar](15) NULL,
	[thirdname] [varchar](15) NULL,
	[email] [varchar](30) NULL,
	[address] [varchar](100) NULL,
	[phone_code] [varchar](6) NULL,
	[phone] [int] NULL,
	[gender] [varchar](10) NULL,
	[age] [int] NULL,
	[access] [int] NULL,
CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED (
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_gender]  DEFAULT ('Мужской') FOR [gender]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [CK_Employees_Age] CHECK  (([age]>(0)))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [CK_Employees_Age]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [CK_Employees_Gender] CHECK  (([gender]='Мужской' OR [gender]='Женский'))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [CK_Employees_Gender]
GO
CREATE TABLE [dbo].[Expert](
	[expertNum] [int] NOT NULL,
	[mark1_2] [int] NULL,
	[mark1_3] [int] NULL,
	[mark1_4] [int] NULL,
	[mark2_3] [int] NULL,
	[mark2_4] [int] NULL,
	[mark3_4] [int] NULL,
CONSTRAINT [PK_Expert] PRIMARY KEY CLUSTERED (
	[expertNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leasing](
	[operation_id] [int] IDENTITY(1,1) NOT NULL,
	[clientFIO] [varchar](50) NULL,
	[emplFIO] [varchar](50) NULL,
[carBrandName] [varchar](50) NULL,
 [operation_date] [date] NULL,
CONSTRAINT [PK_Leasing] PRIMARY KEY CLUSTERED (
	[operation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Leasing] ADD  CONSTRAINT [DF_Leasing_operation_date]  DEFAULT (getdate()) FOR [operation_date]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[gender] [varchar](20) NULL,
	[surname] [varchar](20) NULL,
	[name] [varchar](20) NULL,
	[thirdname] [varchar](20) NULL,
	[age] [int] NULL,
	[email] [varchar](30) NULL,
	[address] [varchar](30) NULL,
	[phone_code] [varchar](10) NULL,
	[phone] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED (
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_gender]  DEFAULT ('Мужской') FOR [gender]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_Gender] CHECK  (([gender]='Мужской' OR [gender]='Женский'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_Gender]
GO