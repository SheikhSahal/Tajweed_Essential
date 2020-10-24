USE [master]
GO
/****** Object:  Database [Tajweed]    Script Date: 10/24/2020 4:51:32 PM ******/
CREATE DATABASE [Tajweed]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tajweed', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Tajweed.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Tajweed_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Tajweed_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Tajweed] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tajweed].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tajweed] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tajweed] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tajweed] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tajweed] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tajweed] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tajweed] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tajweed] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tajweed] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tajweed] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tajweed] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tajweed] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tajweed] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tajweed] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tajweed] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tajweed] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Tajweed] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tajweed] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tajweed] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tajweed] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tajweed] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tajweed] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tajweed] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tajweed] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Tajweed] SET  MULTI_USER 
GO
ALTER DATABASE [Tajweed] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tajweed] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tajweed] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tajweed] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Tajweed] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Tajweed]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attendance](
	[Att_id] [int] NOT NULL,
	[bh_id] [int] NULL,
	[created_by] [varchar](50) NULL,
	[created_date] [date] NULL,
	[Delete_flag] [char](1) NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[Att_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attendance_details]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attendance_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Att_id] [int] NULL,
	[Stud_id] [int] NULL,
	[Att_status] [varchar](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Batch_details]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch_details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BH_ID] [int] NULL,
	[STU_ID] [int] NOT NULL,
 CONSTRAINT [PK_Batch_details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Batch_header]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Batch_header](
	[BH_ID] [int] IDENTITY(1,1) NOT NULL,
	[BATCH_NAME] [varchar](60) NULL,
	[TEACHER_1] [int] NULL,
	[CREATED_BY] [varchar](50) NULL,
	[CREATED_DATE] [date] NULL,
	[Delete_flag] [char](1) NULL,
	[bh_end_date] [date] NULL,
	[bh_start_date] [date] NULL,
	[TEACHER_2] [varchar](50) NULL,
	[TEACHER_3] [varchar](50) NULL,
	[TEACHER_4] [varchar](50) NULL,
	[TEACHER_5] [varchar](50) NULL,
	[Course_desc] [varchar](500) NULL,
	[Course_visible] [char](1) NULL,
 CONSTRAINT [PK_Batch_header] PRIMARY KEY CLUSTERED 
(
	[BH_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Hlp_details]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hlp_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[hpl_id] [int] NULL,
	[stud_id] [int] NULL,
	[hlp_stud_id] [int] NULL,
 CONSTRAINT [PK_Hlp_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hlp_header]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hlp_header](
	[hlp_id] [int] NOT NULL,
	[bh_id] [int] NULL,
	[List_id] [int] NULL,
	[created_by] [varchar](50) NULL,
	[created_date] [date] NULL,
	[Delete_flag] [char](1) NULL,
	[Helper_name] [varchar](50) NULL,
 CONSTRAINT [PK_Hlp_header] PRIMARY KEY CLUSTERED 
(
	[hlp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[List_details]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[List_id] [int] NULL,
	[Stud_id] [int] NULL,
 CONSTRAINT [PK_List_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[List_header]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[List_header](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[List_id] [int] NULL,
	[List_name] [varchar](50) NULL,
 CONSTRAINT [PK_List_header] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[User_name] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[User_email] [varchar](50) NOT NULL,
	[User_contact] [varchar](15) NULL,
	[User_Profile] [varchar](500) NULL,
	[Pass_exp] [date] NULL,
	[Last_login] [date] NULL,
	[User_Active] [char](1) NULL CONSTRAINT [DF_Login_User_Active]  DEFAULT ('Y'),
	[Role_id] [int] NULL CONSTRAINT [DF_Login_Role_id]  DEFAULT ((2)),
	[DOB] [date] NULL,
	[Martial_status] [char](30) NULL,
	[F_H_name] [varchar](50) NULL,
	[ID_Card] [varchar](30) NULL,
	[Address] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Qualification] [varchar](50) NULL,
	[Profession] [varchar](50) NULL,
	[Q_A] [varchar](50) NULL,
	[Future_Plan] [varchar](50) NULL,
	[recommended] [varchar](50) NULL,
	[User_status] [char](1) NULL CONSTRAINT [DF_Login_User_status]  DEFAULT ('W'),
	[Bh_id] [int] NULL,
	[User_flag] [char](1) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[Menu_id] [int] IDENTITY(1,1) NOT NULL,
	[Menu_name] [varchar](50) NULL,
	[Menu_URL] [varchar](50) NULL,
	[Menu_parent_id] [int] NULL,
	[Menu_Active] [char](1) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Registration](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FULL_NAME] [varchar](30) NULL,
	[DATE_OF_BIRTH] [date] NULL,
	[MARITAL_STATUS] [varchar](1) NULL,
	[FATHR_HUS_NAME] [varchar](30) NULL,
	[ID_CARD_NO] [int] NULL,
	[MOBILE_NO] [int] NULL,
	[EMAIL] [varchar](50) NULL,
	[ADDRESS] [varchar](100) NULL,
	[CITY] [varchar](30) NULL,
	[COUNTRY] [varchar](30) NULL,
	[QUALIFICATION] [char](2) NULL,
	[PROFESSION] [varchar](30) NULL,
	[QURN_EDU] [varchar](100) NULL,
	[FUTURE_PLAN] [varchar](100) NULL,
	[RECMM_PERSN] [varchar](40) NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Role_id] [int] IDENTITY(1,1) NOT NULL,
	[Role_name] [varchar](50) NULL,
	[Role_Active] [char](1) NULL,
	[Menu_id] [int] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teacher](
	[Teach_id] [int] IDENTITY(1,1) NOT NULL,
	[Teach_name] [varchar](50) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Teach_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_privilege]    Script Date: 10/24/2020 4:51:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_privilege](
	[Role_id] [int] NULL,
	[Menu_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([User_id], [User_name], [Password], [User_email], [User_contact], [User_Profile], [Pass_exp], [Last_login], [User_Active], [Role_id], [DOB], [Martial_status], [F_H_name], [ID_Card], [Address], [Country], [Qualification], [Profession], [Q_A], [Future_Plan], [recommended], [User_status], [Bh_id], [User_flag]) VALUES (1030, N'Sahal', N'1234', N's.m.sahal786@outlook.com', N'0', NULL, NULL, NULL, N'Y', 1, CAST(N'1996-07-20' AS Date), N'Single                        ', N'Qasim', N'4220167636761', N'house no', N'Pakistan', N'master''s', N'software engineer', N'no', N'no', N'no', N'W', 1, N'U')
SET IDENTITY_INSERT [dbo].[Login] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (10, N'Dashboard', N'Dashboard', NULL, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (11, N'Admin', NULL, NULL, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (12, N'Course', N'Course_List/Index', 11, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (13, N'Helper', N'New_Helper/Index', 11, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (14, N'Attendance', N'New_attendance/Index', 11, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (15, N'Report', NULL, NULL, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (16, N'Student Report', N'Report_param/Index', 15, N'Y')
INSERT [dbo].[Menu] ([Menu_id], [Menu_name], [Menu_URL], [Menu_parent_id], [Menu_Active]) VALUES (18, N'List', N'List_header/Index', 11, N'Y')
SET IDENTITY_INSERT [dbo].[Menu] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Role_id], [Role_name], [Role_Active], [Menu_id]) VALUES (1, N'Admin', N'Y', NULL)
INSERT [dbo].[Role] ([Role_id], [Role_name], [Role_Active], [Menu_id]) VALUES (2, N'User', N'Y', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Teach_id], [Teach_name]) VALUES (4, N'Miss Nida')
INSERT [dbo].[Teacher] ([Teach_id], [Teach_name]) VALUES (5, N'MIss Mahnoor')
INSERT [dbo].[Teacher] ([Teach_id], [Teach_name]) VALUES (6, N'Miss kulsoom')
SET IDENTITY_INSERT [dbo].[Teacher] OFF
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 10)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 11)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 12)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 13)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 14)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 15)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 16)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (2, 10)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 17)
INSERT [dbo].[User_privilege] ([Role_id], [Menu_id]) VALUES (1, 18)
ALTER TABLE [dbo].[Attendance] ADD  CONSTRAINT [DF_Attendance_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[Batch_header] ADD  CONSTRAINT [DF_Batch_header_CREATED_DATE]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
ALTER TABLE [dbo].[Batch_header] ADD  CONSTRAINT [DF_Batch_header_Course_visible]  DEFAULT ('N') FOR [Course_visible]
GO
ALTER TABLE [dbo].[Hlp_header] ADD  CONSTRAINT [DF_Hlp_header_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK_Login_Role] FOREIGN KEY([Role_id])
REFERENCES [dbo].[Role] ([Role_id])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK_Login_Role]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Menu] FOREIGN KEY([Menu_id])
REFERENCES [dbo].[Menu] ([Menu_id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Menu]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Role] FOREIGN KEY([Role_id])
REFERENCES [dbo].[Role] ([Role_id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Role]
GO
USE [master]
GO
ALTER DATABASE [Tajweed] SET  READ_WRITE 
GO
