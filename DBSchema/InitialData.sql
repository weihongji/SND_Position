DECLARE @enforce bit
SET @enforce = 0


IF @enforce = 1 BEGIN
	TRUNCATE TABLE MonitorSystem
END

IF NOT EXISTS(SELECT * FROM MonitorSystem) BEGIN
	SET IDENTITY_INSERT MonitorSystem ON
	
	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (1, N'安全监测', 1)

	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (2, N'风压自救', 1)

	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (3, N'供水施救系统', 1)

	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (4, N'紧急避难系统', 1)

	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (5, N'报表系统', 1)

	INSERT INTO MonitorSystem (Id, Name, CompanyMineId)
	VALUES (6, N'人员定位系统', 1)
	
	SET IDENTITY_INSERT MonitorSystem OFF
END

IF @enforce = 1 BEGIN
	TRUNCATE TABLE MonitorContent
END

IF NOT EXISTS(SELECT * FROM MonitorContent) BEGIN
	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'地图起始点', 0, 'pin-start.png', 'dot-red.png', 9, 27)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'氮气', 1, 'pin-blue.png', 'dot-blue.png', 0, 60)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'二氧化碳', 1, 'pin-red.png', 'dot-red.png', 0, 60)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'瓦斯', 1, 'pin-green.png', 'dot-green.png', 0, 60)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'风压', 2, 'pin-blue.png', 'dot-blue.png', 0, 60)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'供水', 3, 'pin-blue.png', 'dot-blue.png', 0, 60)

	INSERT INTO MonitorContent ([Name], [MonitorSystemId], [Image], [ImageOverview], [PointerX], [PointerY])
	VALUES (N'接收器', 6, 'pin-blue.png', 'dot-blue.png', 0, 60)
END

IF @enforce = 1 BEGIN
	TRUNCATE TABLE MonitorMap
END
IF NOT EXISTS(SELECT * FROM MonitorMap) BEGIN
	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (1, 'Map_1_1.gif', 846, 137, 3000, 1, 'Map_1_1.gif')

	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (1, 'Map_1_2.gif', 1692, 274, 2000, 2, 'Map_1_2.gif')

	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (1, 'Map_1_3.gif', 2538, 411, 1000, 3, 'Map_1_3.gif')

	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (2, 'Map_2_1.gif', 846, 137, 3000, 1, 'Map_2_1.gif')

	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (2, 'Map_2_2.gif', 1692, 274, 2000, 2, 'Map_2_2.gif')

	INSERT INTO MonitorMap (MonitorSystemId, Name, StartX, StartY, Scale, SizeType, DisplayName)
	VALUES (2, 'Map_2_3.gif', 2538, 411, 1000, 3, 'Map_2_3.gif')
END