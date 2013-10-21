IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[WorkType]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[WorkType](
		[Worktype_id] [smallint] NOT NULL,
		[Worktype_type] [tinyint] NOT NULL,
		[Worktype_name] [varchar](32) NOT NULL,
	 CONSTRAINT [PK_WorkType] PRIMARY KEY CLUSTERED 
	(
		[Worktype_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Rank]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Rank](
		[Rank_id] [smallint] NOT NULL,
		[Rank_type] [tinyint] NOT NULL,
		[Rank_name] [varchar](32) NOT NULL,
	 CONSTRAINT [PK_Rank] PRIMARY KEY CLUSTERED 
	(
		[Rank_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Department](
		[Dept_id] [smallint] NOT NULL,
		[Dept_name] [varchar](32) NOT NULL,
		[Dept_fullname] [varchar](64) NOT NULL,
		[Address] [varchar](64) NOT NULL,
		[Phone] [varchar](32) NOT NULL,
		[Dept_info] [varchar](64) NOT NULL,
	 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
	(
		[Dept_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[People]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[People](
		[People_id] [int] NOT NULL,
		[People_name] [varchar](32) NOT NULL,
		[Gender] [tinyint] NOT NULL,
		[Picture] [image] NULL,
		[Dept_id] [smallint] NOT NULL,
		[Worktype_id] [smallint] NOT NULL,
		[Rank_id] [smallint] NOT NULL,
		[Birthday] [datetime] NOT NULL,
		[Enroll_time] [datetime] NOT NULL,
		[Address] [varchar](64) NOT NULL,
		[Phone] [varchar](32) NOT NULL,
		[ID_Number] [varchar](18) NOT NULL,
		[Blood_type] [tinyint] NOT NULL,
		[Allergy] [varchar](64) NOT NULL,
		[Linkman_name] [varchar](32) NOT NULL,
		[Linkman_dept] [varchar](64) NOT NULL,
		[Linkman_phone] [varchar](32) NOT NULL,
		[People_info] [varchar](64) NOT NULL,
	 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
	(
		[People_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[People]  WITH CHECK ADD FOREIGN KEY([Dept_id])
	REFERENCES [dbo].[Department] ([Dept_id])

	ALTER TABLE [dbo].[People]  WITH CHECK ADD FOREIGN KEY([Rank_id])
	REFERENCES [dbo].[Rank] ([Rank_id])

	ALTER TABLE [dbo].[People]  WITH CHECK ADD FOREIGN KEY([Worktype_id])
	REFERENCES [dbo].[WorkType] ([Worktype_id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Product](
		[Product_id] [smallint] NOT NULL,
		[Parameters] [binary](8) NULL,
		[Product_desc] [varchar](64) NULL,
	 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
	(
		[Product_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Sender]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Sender](
		[Sender_id] [int] NOT NULL,
		[Sender_type] [tinyint] NOT NULL,
		[Status] [smallint] NOT NULL,
		[Product_id] [smallint] NOT NULL,
		[Parameters] [binary](8) NULL,
		[First_use_time] [datetime] NOT NULL,
		[Last_use_time] [datetime] NOT NULL,
		[Sender_info] [varchar](64) NULL,
	 CONSTRAINT [PK_Sender] PRIMARY KEY CLUSTERED 
	(
		[Sender_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Sender]  WITH CHECK ADD FOREIGN KEY([Product_id])
	REFERENCES [dbo].[Product] ([Product_id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[PeopleSender]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[PeopleSender](
		[People_id] [int] NOT NULL,
		[Sender_id] [int] NOT NULL,
		[First_use_time] [datetime] NOT NULL,
		[Last_use_time] [datetime] NOT NULL
	) ON [PRIMARY]


	ALTER TABLE [dbo].[PeopleSender]  WITH CHECK ADD FOREIGN KEY([Sender_id])
	REFERENCES [dbo].[Sender] ([Sender_id])

	ALTER TABLE [dbo].[PeopleSender]  WITH CHECK ADD  CONSTRAINT [FK_PeopleSender] FOREIGN KEY([People_id])
	REFERENCES [dbo].[People] ([People_id])

	ALTER TABLE [dbo].[PeopleSender] CHECK CONSTRAINT [FK_PeopleSender]
END


IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[ShiftGroup]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[ShiftGroup](
		[ShiftGroupId] [smallint] IDENTITY(0,1) NOT NULL,
		[ShiftGroupName] [varchar](32) NULL,
	 CONSTRAINT [PK_ShiftGroup] PRIMARY KEY CLUSTERED 
	(
		[ShiftGroupId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Shift]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Shift](
		[ShiftId] [smallint] IDENTITY(0,1) NOT NULL,
		[ShiftName] [varchar](32) NOT NULL,
		[ShiftBeginTime] [datetime] NOT NULL,
		[ShiftEndTime] [datetime] NOT NULL,
		[ShiftGroupId] [smallint] NULL,
	 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
	(
		[ShiftId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Shift]  WITH CHECK ADD FOREIGN KEY([ShiftGroupId])
	REFERENCES [dbo].[ShiftGroup] ([ShiftGroupId])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[PeopleShift]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[PeopleShift](
		[PeopleId] [int] NOT NULL,
		[ShiftId] [smallint] NOT NULL,
		[FirstShiftTime] [datetime] NOT NULL,
		[LastShiftTime] [datetime] NOT NULL,
	 CONSTRAINT [PK_PeopleShift] PRIMARY KEY CLUSTERED 
	(
		[PeopleId] ASC,
		[FirstShiftTime] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[PeopleShift]  WITH CHECK ADD FOREIGN KEY([PeopleId])
	REFERENCES [dbo].[People] ([People_id])

	ALTER TABLE [dbo].[PeopleShift]  WITH CHECK ADD FOREIGN KEY([ShiftId])
	REFERENCES [dbo].[Shift] ([ShiftId])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Position]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Position](
		[Position_id] [smallint] NOT NULL,
		[Position_x] [int] NOT NULL,
		[Position_y] [int] NOT NULL,
		[Position_z] [int] NOT NULL,
		[Position_sin] [float] NOT NULL,
		[Position_cos] [float] NOT NULL,
		[Position_vcos] [float] NOT NULL,
		[Position_desc] [varchar](64) NULL,
	 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
	(
		[Position_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[PeopleWorkPath]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[PeopleWorkPath](
		[Path_id] [int] NOT NULL,
		[Step_id] [int] NOT NULL,
		[People_id] [int] NOT NULL,
		[Position_id] [smallint] NOT NULL,
		[Begin_time] [datetime] NOT NULL,
		[End_time] [datetime] NOT NULL,
		[Check_status] [tinyint] NOT NULL,
	 CONSTRAINT [PK_PeopleWorkPath] PRIMARY KEY CLUSTERED 
	(
		[Path_id] ASC,
		[Step_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[PeopleWorkPath]  WITH CHECK ADD FOREIGN KEY([People_id])
	REFERENCES [dbo].[People] ([People_id])

	ALTER TABLE [dbo].[PeopleWorkPath]  WITH CHECK ADD FOREIGN KEY([Position_id])
	REFERENCES [dbo].[Position] ([Position_id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Lamp]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Lamp](
		[Lamp_id] [varchar](8) NOT NULL,
		[Sender_id] [int] NOT NULL,
		[Lamp_info] [varchar](64) NOT NULL,
	 CONSTRAINT [PK_Lamp] PRIMARY KEY CLUSTERED 
	(
		[Lamp_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Lamp]  WITH CHECK ADD FOREIGN KEY([Sender_id])
	REFERENCES [dbo].[Sender] ([Sender_id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Region](
		[Region_id] [smallint] NOT NULL,
		[Region_name] [varchar](32) NOT NULL,
		[Region_type] [tinyint] NOT NULL,
		[People_max] [smallint] NOT NULL,
		[Linger_max] [smallint] NOT NULL,
		[Status] [smallint] NOT NULL,
		[Display_x] [int] NOT NULL,
		[Display_y] [int] NOT NULL,
		[Display_type] [tinyint] NOT NULL,
		[Region_info] [varchar](64) NULL,
	 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
	(
		[Region_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Branch]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Branch](
		[Branch_id] [tinyint] NOT NULL,
		[Status] [smallint] NOT NULL,
		[Product_id] [smallint] NOT NULL,
		[Position_id] [smallint] NOT NULL,
		[Comm_mode] [tinyint] NOT NULL,
		[Can_no] [tinyint] NOT NULL,
		[Uart_port] [tinyint] NOT NULL,
		[Ip] [int] NOT NULL,
		[Ip_port] [smallint] NOT NULL,
		[Parameters] [binary](512) NULL,
		[Branch_info] [varchar](64) NULL,
	 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
	(
		[Branch_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[Branch]  WITH CHECK ADD FOREIGN KEY([Position_id])
	REFERENCES [dbo].[Position] ([Position_id])


	ALTER TABLE [dbo].[Branch]  WITH CHECK ADD FOREIGN KEY([Product_id])
	REFERENCES [dbo].[Product] ([Product_id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[Receiver]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[Receiver](
		[Branch_id] [tinyint] NOT NULL,
		[Receiver_id] [tinyint] NOT NULL,
		[Status] [smallint] NOT NULL,
		[Product_id] [smallint] NOT NULL,
		[Position_id] [smallint] NOT NULL,
		[Parameters] [binary](512) NULL,
		[Receiver_info] [varchar](64) NULL,
	 CONSTRAINT [PK_Receiver] PRIMARY KEY CLUSTERED 
	(
		[Branch_id] ASC,
		[Receiver_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD FOREIGN KEY([Branch_id])
	REFERENCES [dbo].[Branch] ([Branch_id])

	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD FOREIGN KEY([Position_id])
	REFERENCES [dbo].[Position] ([Position_id])

	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD FOREIGN KEY([Product_id])
	REFERENCES [dbo].[Product] ([Product_id])
END
