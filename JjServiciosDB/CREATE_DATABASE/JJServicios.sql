USE [JJServicios]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getutcdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identification] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FinancialNumber] [bigint] NOT NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[OtherPhone] [nvarchar](20) NULL,
	[WhatsApp] [nvarchar](20) NULL,
	[Skype] [nvarchar](50) NULL,
	[CorporateEmail] [nvarchar](50) NOT NULL,
	[OtherEmail] [nvarchar](50) NULL,
	[ResidenceCity] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[Birthdate] [date] NULL,
	[EmployeePositionId] [bigint] NOT NULL,
	[FinancialAccountId] [bigint] NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getutcdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosition](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getutcdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Expense]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Observations] [nvarchar](50) NOT NULL,
	[MovementTypeId] [bigint] NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancialAccount]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialAccount](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getutcdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_FinancialAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Income]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Observations] [nvarchar](50) NOT NULL,
	[MovementTypeId] [bigint] NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Income] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MovementType]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovementType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MovementType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentEmployee]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentEmployee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Observations] [nvarchar](50) NOT NULL,
	[Support] [nvarchar](50) NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PaymentEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceMovement]    Script Date: 7/8/2017 8:16:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceMovement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[STFRequirement] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Observations] [nvarchar](50) NOT NULL,
	[Support] [nvarchar](50) NOT NULL,
	[MovementTypeId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[AgentId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ServiceMovement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Agent] ON 

INSERT [dbo].[Agent] ([Id], [Name], [Email], [Password], [CreatedDate], [UpdateDate]) VALUES (1, N'Default', N'a@a.com', N'Default', CAST(N'2017-07-08 13:08:33.033' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Agent] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Identification], [Name], [LastName], [FinancialNumber], [Mobile], [OtherPhone], [WhatsApp], [Skype], [CorporateEmail], [OtherEmail], [ResidenceCity], [Address], [Active], [Birthdate], [EmployeePositionId], [FinancialAccountId], [AgentId], [CreatedDate], [UpdateDate]) VALUES (4, N'75101616', N'Julián David', N'Arroyave Zuluaga', 442163366, N'3108331736', N'8882384', N'3108331736', N'juliandavid@live.com', N'julian.arroyave@newshore.es', N'juliandavid@live.com', N'Medellín', N'Carrera 82 N° 47 - 150 Apto 201', 1, CAST(N'1984-07-15' AS Date), 1, 8, 1, CAST(N'2017-07-08 13:12:57.620' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[EmployeePosition] ON 

INSERT [dbo].[EmployeePosition] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (1, N'Socio', 1, CAST(N'2017-07-08 13:09:10.173' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmployeePosition] OFF
SET IDENTITY_INSERT [dbo].[FinancialAccount] ON 

INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (1, N'Helm Banck', 1, CAST(N'2017-07-08 13:10:01.557' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (2, N'Davivienda', 1, CAST(N'2017-07-08 13:10:08.590' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (3, N'Citibank', 1, CAST(N'2017-07-08 13:10:13.733' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (4, N'BCS', 1, CAST(N'2017-07-08 13:10:20.380' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (5, N'Occidente', 1, CAST(N'2017-07-08 13:10:37.050' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (6, N'Sector', 1, CAST(N'2017-07-08 13:10:48.053' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (7, N'Popular', 1, CAST(N'2017-07-08 13:10:54.690' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (8, N'BBVA', 1, CAST(N'2017-07-08 13:11:01.883' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (9, N'Colpatria', 1, CAST(N'2017-07-08 13:11:11.683' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (10, N'Av Villas', 1, CAST(N'2017-07-08 13:11:19.067' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (11, N'Corpbanca', 1, CAST(N'2017-07-08 13:11:26.760' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (12, N'Bancolombia', 1, CAST(N'2017-07-08 13:11:33.290' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (15, N'Bogotá', 1, CAST(N'2017-07-08 13:11:48.017' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (16, N'Nequi', 1, CAST(N'2017-07-08 13:12:11.283' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (18, N'Daviplata', 1, CAST(N'2017-07-08 13:12:23.810' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
INSERT [dbo].[FinancialAccount] ([Id], [Name], [AgentId], [CreatedDate], [UpdateDate]) VALUES (19, N'BBVA Móvil', 1, CAST(N'2017-07-08 13:12:34.803' AS DateTime), CAST(N'2017-07-08 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[FinancialAccount] OFF
ALTER TABLE [dbo].[Expense] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Income] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MovementType] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PaymentEmployee] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceMovement] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Agent]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeePosition] FOREIGN KEY([EmployeePositionId])
REFERENCES [dbo].[EmployeePosition] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeePosition]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_FinancialAccount] FOREIGN KEY([FinancialAccountId])
REFERENCES [dbo].[FinancialAccount] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_FinancialAccount]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_Agent1] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_Agent1]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Agent]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_MovementType] FOREIGN KEY([MovementTypeId])
REFERENCES [dbo].[MovementType] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_MovementType]
GO
ALTER TABLE [dbo].[FinancialAccount]  WITH CHECK ADD  CONSTRAINT [FK_FinancialAccount_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[FinancialAccount] CHECK CONSTRAINT [FK_FinancialAccount_Agent]
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[Income] CHECK CONSTRAINT [FK_Income_Agent]
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD  CONSTRAINT [FK_Income_MovementType] FOREIGN KEY([MovementTypeId])
REFERENCES [dbo].[MovementType] ([Id])
GO
ALTER TABLE [dbo].[Income] CHECK CONSTRAINT [FK_Income_MovementType]
GO
ALTER TABLE [dbo].[MovementType]  WITH CHECK ADD  CONSTRAINT [FK_MovementType_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[MovementType] CHECK CONSTRAINT [FK_MovementType_Agent]
GO
ALTER TABLE [dbo].[PaymentEmployee]  WITH CHECK ADD  CONSTRAINT [FK_PaymentEmployee_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[PaymentEmployee] CHECK CONSTRAINT [FK_PaymentEmployee_Agent]
GO
ALTER TABLE [dbo].[PaymentEmployee]  WITH CHECK ADD  CONSTRAINT [FK_PaymentEmployee_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[PaymentEmployee] CHECK CONSTRAINT [FK_PaymentEmployee_Employee]
GO
ALTER TABLE [dbo].[ServiceMovement]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMovement_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id])
GO
ALTER TABLE [dbo].[ServiceMovement] CHECK CONSTRAINT [FK_ServiceMovement_Agent]
GO
ALTER TABLE [dbo].[ServiceMovement]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMovement_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[ServiceMovement] CHECK CONSTRAINT [FK_ServiceMovement_Employee]
GO
ALTER TABLE [dbo].[ServiceMovement]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMovement_MovementType] FOREIGN KEY([MovementTypeId])
REFERENCES [dbo].[MovementType] ([Id])
GO
ALTER TABLE [dbo].[ServiceMovement] CHECK CONSTRAINT [FK_ServiceMovement_MovementType]
GO
