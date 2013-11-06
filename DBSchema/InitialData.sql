IF NOT EXISTS(SELECT * FROM sys.tables WHERE  object_id = OBJECT_ID(N'[dbo].[MonitorType]') AND type in (N'U')) BEGIN
	CREATE TABLE [dbo].[MonitorType](
		[Id] [int] NOT NULL CONSTRAINT [PK_MonitorType] PRIMARY KEY CLUSTERED,
		[Name] [nvarchar](50) NOT NULL,
		[Image] [varchar](50) NULL, /*Name of the image file*/
		[OffsetX] [int] NOT NULL CONSTRAINT [DF_MonitorType_OffsetX] DEFAULT (0), /*Offset in pixel of the pointer to the Image position-left.*/
		[OffsetY] [int] NOT NULL CONSTRAINT [DF_MonitorType_OffsetY] DEFAULT (0), /*Offset in pixel of the pointer to the Image position-top.*/
	) ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM MonitorType) BEGIN
	INSERT INTO MonitorType ([Id], [Name], [Image], [OffsetX], [OffsetY])
	VALUES (1, '风压检测', 'pin-blue.png', 0, 60)

	INSERT INTO MonitorType ([Id], [Name], [Image], [OffsetX], [OffsetY])
	VALUES (2, '瓦斯检测', 'pin-red.png', 0, 60)

	INSERT INTO MonitorType ([Id], [Name], [Image], [OffsetX], [OffsetY])
	VALUES (3, '水压检测', 'pin-green.png', 0, 60)
END