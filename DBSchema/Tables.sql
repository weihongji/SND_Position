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
		[Company_id] [smallint] NOT NULL,
		[Dept_id] [smallint] NOT NULL,
		[Dept_name] [varchar](32) NOT NULL,
		[Dept_fullname] [varchar](64) NOT NULL,
		[Address] [varchar](64) NOT NULL,
		[Phone] [varchar](32) NOT NULL,
		[Dept_info] [varchar](64) NOT NULL,
	 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
	(
		[Company_id] ASC,
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


	CREATE NONCLUSTERED INDEX [PeopleIndex] ON [dbo].[People] 
	(
		[Dept_id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [PeopleIndex2] ON [dbo].[People] 
	(
		[People_name] ASC
	)

	CREATE NONCLUSTERED INDEX [PeopleIndex3] ON [dbo].[People] 
	(
		[Worktype_id] ASC
	)

	CREATE NONCLUSTERED INDEX [PeopleIndex4] ON [dbo].[People] 
	(
		[Rank_id] ASC
	)
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
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[PeopleSender]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[PeopleSender](
		[People_id] [int] NOT NULL,
		[Sender_id] [int] NOT NULL,
		[First_use_time] [datetime] NOT NULL,
		[Last_use_time] [datetime] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [PeopleSenderIndex] ON [dbo].[PeopleSender] 
	(
		[Sender_id] ASC,
		[Last_use_time] ASC
	)

	CREATE NONCLUSTERED INDEX [PeopleSenderIndex2] ON [dbo].[PeopleSender] 
	(
		[Sender_id] ASC,
		[First_use_time] ASC
	)

	CREATE NONCLUSTERED INDEX [PeopleSenderIndex3] ON [dbo].[PeopleSender] 
	(
		[People_id] ASC,
		[Last_use_time] ASC
	)

	CREATE NONCLUSTERED INDEX [PeopleSenderIndex4] ON [dbo].[PeopleSender] 
	(
		[People_id] ASC,
		[First_use_time] ASC
	)
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

	CREATE NONCLUSTERED INDEX [BranchIndex] ON [dbo].[Branch] 
	(
		[Comm_mode] ASC
	)
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
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[RegionPositionSet]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[RegionPositionSet](
		[Region_id] [smallint] NOT NULL,
		[Position_id] [smallint] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [RegionPositionSetIndex] ON [dbo].[RegionPositionSet] 
	(
		[Region_id] ASC
	)
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[PositionReport]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[PositionReport](
		[Sender_id] [int] NOT NULL,
		[Branch_id] [tinyint] NOT NULL,
		[Receiver_id] [tinyint] NOT NULL,
		[Position_id] [smallint] NOT NULL,
		[Distance] [smallint] NOT NULL,
		[Report_time] [datetime] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [PositionReportIndex] ON [dbo].[PositionReport] 
	(
		[Report_time] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [PositionReportIndex2] ON [dbo].[PositionReport] 
	(
		[Sender_id] ASC,
		[Report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [PositionReportIndex3] ON [dbo].[PositionReport] 
	(
		[Branch_id] ASC,
		[Receiver_id] ASC,
		[Report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [PositionReportIndex4] ON [dbo].[PositionReport] 
	(
		[Position_id] ASC,
		[Report_time] ASC
	)
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[CurrentPositionReport]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[CurrentPositionReport](
		[Sender_id] [int] NOT NULL,
		[Branch_id] [tinyint] NOT NULL,
		[Receiver_id] [tinyint] NOT NULL,
		[Position_id] [smallint] NOT NULL,
		[Distance] [smallint] NOT NULL,
		[Report_time] [datetime] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [CurrentPositionReportIndex] ON [dbo].[CurrentPositionReport] 
	(
		[Sender_id] ASC,
		[Report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [CurrentPositionReportIndex2] ON [dbo].[CurrentPositionReport] 
	(
		[Position_id] ASC,
		[Sender_id] ASC
	)
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[AlarmReport]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[AlarmReport](
		[Alarm_id] [int] NOT NULL,
		[Alarm_type] [tinyint] NOT NULL,
		[Alarm_param1] [int] NOT NULL,
		[Alarm_param2] [int] NOT NULL,
		[First_report_time] [datetime] NOT NULL,
		[Last_report_time] [datetime] NOT NULL,
		[Process_time] [datetime] NOT NULL,
		[Login_name] [varchar](32) NOT NULL,
		[Process_status] [tinyint] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [AlarmReportIndex] ON [dbo].[AlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[Last_report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [AlarmReportIndex2] ON [dbo].[AlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[First_report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [AlarmReportIndex3] ON [dbo].[AlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[Process_time] ASC
	)
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[AlarmType]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[AlarmType](
		[Alarm_type] [tinyint] NOT NULL,
		[Alarm_name] [varchar](32) NOT NULL,
		[Param1_name] [varchar](32) NOT NULL,
		[Param2_name] [varchar](32) NOT NULL,
		[Alarm_level] [tinyint] NOT NULL,
		[Alarm_attrib] [tinyint] NOT NULL,
		[Valid_seconds] [int] NOT NULL,
		[Auto_recovery_seconds] [int] NOT NULL,
	 CONSTRAINT [PK_AlarmType] PRIMARY KEY CLUSTERED 
	(
		[Alarm_type] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[CurrentAlarmReport]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[CurrentAlarmReport](
		[Alarm_id] [int] NOT NULL,
		[Alarm_type] [tinyint] NOT NULL,
		[Alarm_param1] [int] NOT NULL,
		[Alarm_param2] [int] NOT NULL,
		[First_report_time] [datetime] NOT NULL,
		[Last_report_time] [datetime] NOT NULL,
		[Process_time] [datetime] NOT NULL,
		[Login_name] [varchar](32) NOT NULL,
		[Process_status] [tinyint] NOT NULL
	) ON [PRIMARY]

	CREATE CLUSTERED INDEX [CurrentAlarmReportIndex] ON [dbo].[CurrentAlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[Last_report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [CurrentAlarmReportIndex2] ON [dbo].[CurrentAlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[First_report_time] ASC
	)

	CREATE NONCLUSTERED INDEX [CurrentAlarmReportIndex3] ON [dbo].[CurrentAlarmReport] 
	(
		[Alarm_type] ASC,
		[Alarm_param1] ASC,
		[Alarm_param2] ASC,
		[Process_time] ASC
	)
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[MonitorSystem]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[MonitorSystem](
		[Id] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_MonitorSystem] PRIMARY KEY CLUSTERED ,
		[Name] [nvarchar](100) NULL,
		[CompanyMineId] [bigint] NULL,
		[Information] [nvarchar](100) NULL,
		[Remark] [nvarchar](200) NULL,
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[MonitorContent]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[MonitorContent](
		[Id] [bigint] IDENTITY(0,1) NOT NULL CONSTRAINT [PK_MonitorContent] PRIMARY KEY CLUSTERED,
		[Name] [nvarchar](50) NULL,
		[MonitorSystemId] [int] NULL,
		[Information] [nvarchar](100) NULL,
		[Remark] [nvarchar](200) NULL,
		[Image] [varchar](50) NULL, /*Name of the image file*/
		[ImageOverview] [varchar](50) NULL, /*Name of the image file shown on overview area */
		[PointerX] [int] NOT NULL CONSTRAINT [DF_MonitorContent_PointerX] DEFAULT 0, /*Offset in pixel of the pointer to the Image position-left.*/
		[PointerY] [int] NOT NULL CONSTRAINT [DF_MonitorContent_PointerY] DEFAULT 0 /*Offset in pixel of the pointer to the Image position-top.*/
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[MonitorPoint]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[MonitorPoint](
		[Id] [bigint] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_MonitorPosition] PRIMARY KEY CLUSTERED,
		[Name] [nvarchar](50) NOT NULL,
		[Information] [nvarchar](100) NULL,
		[OffsetX] [int] NOT NULL,
		[OffsetY] [int] NOT NULL,
		[MonitorContentId] [bigint] NOT NULL,
		[AlarmUp] [decimal](10, 2) NULL,
		[AlarmDown] [decimal](10, 2) NULL,
		[RangeUp] [decimal](10, 2) NULL,
		[RangeDown] [decimal](10, 2) NULL,
		[Remark] [nvarchar](100) NULL,
		[DTStamp] [datetime] NOT NULL CONSTRAINT [DF_MonitorPosition_DTStamp]  DEFAULT (getdate()),
	) ON [PRIMARY]

	ALTER TABLE [dbo].[MonitorPoint]  WITH CHECK ADD  CONSTRAINT [FK_MonitorPoint_MonitorContent] FOREIGN KEY([MonitorContentId])
	REFERENCES [dbo].[MonitorContent] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[MonitorMap]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[MonitorMap](
		[Id] [int] IDENTITY(1, 1) NOT NULL CONSTRAINT [PK_MonitorMap] PRIMARY KEY CLUSTERED,
		[MonitorSystemId] [int] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[StartX] [int] NOT NULL,
		[StartY] [int] NOT NULL,
		[Scale] [int] NOT NULL CONSTRAINT [DF_MonitorMap_Scale] DEFAULT (0),
		[DisplayName] [nvarchar](50) NOT NULL,
		[DTStamp] [smalldatetime] NOT NULL CONSTRAINT [DF_MonitorMap_DTStamp] DEFAULT (getdate())
	) ON [PRIMARY]
END