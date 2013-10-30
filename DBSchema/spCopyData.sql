IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spCopyData]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spCopyData]
END
GO

CREATE PROCEDURE [dbo].[spCopyData] (
	@interval varchar(20) = '0:00:00.500'
)
AS
	EXEC spCopyDataPrepare

	/* IMPORT data from external DB */
	TRUNCATE TABLE AlarmReport
	INSERT INTO AlarmReport(Alarm_id, Alarm_type, Alarm_param1, Alarm_param2, First_report_time, Last_report_time, Process_time, Login_name, Process_status)
	SELECT * FROM KJ07..AlarmReport
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Branch
	INSERT INTO Branch SELECT * FROM KJ07..Branch
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE CurrentAlarmReport
	INSERT INTO CurrentAlarmReport(Alarm_id, Alarm_type, Alarm_param1, Alarm_param2, First_report_time, Last_report_time, Process_time, Login_name, Process_status)
	SELECT * FROM KJ07..CurrentAlarmReport
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE CurrentPositionReport
	INSERT INTO CurrentPositionReport SELECT * FROM KJ07..CurrentPositionReport
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Department
	INSERT INTO Department SELECT * FROM KJ07..Department
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Lamp
	INSERT INTO Lamp SELECT * FROM KJ07..Lamp
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE People
	INSERT INTO People SELECT * FROM KJ07..People
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE PeopleSender
	INSERT INTO PeopleSender SELECT * FROM KJ07..PeopleSender
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Position
	INSERT INTO Position SELECT * FROM KJ07..Position
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE PositionReport
	INSERT INTO PositionReport SELECT * FROM KJ07..PositionReport
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Product
	INSERT INTO Product SELECT * FROM KJ07..Product
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE [Rank]
	INSERT INTO [Rank] SELECT * FROM KJ07..[Rank]
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Receiver
	INSERT INTO Receiver SELECT * FROM KJ07..Receiver
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Region
	INSERT INTO Region SELECT * FROM KJ07..Region
	IF @interval IS NOT NULL WAITFOR DELAY @interval


	TRUNCATE TABLE RegionPositionSet
	INSERT INTO RegionPositionSet SELECT * FROM KJ07..RegionPositionSet
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE Sender
	INSERT INTO Sender SELECT * FROM KJ07..Sender
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	TRUNCATE TABLE WorkType
	INSERT INTO WorkType SELECT * FROM KJ07..WorkType
	IF @interval IS NOT NULL WAITFOR DELAY @interval

	EXEC spCopyDataDone