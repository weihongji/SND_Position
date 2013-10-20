IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPeopleCountByRegion]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPeopleCountByRegion]
END
GO

CREATE PROCEDURE [dbo].[spPeopleCountByRegion] (
	@forTime datetime = NULL
)
AS

--SET @forTime = '2013-08-14 10:00'

DECLARE @startTime datetime, @endTime datetime
DECLARE @regionCount AS TABLE (Region_id smallint, Number int)

/* Date & Time criteria */
SET @startTime = DATEADD(MINUTE, -10, @forTime)
SET @endTime = @forTime

INSERT INTO @regionCount
SELECT RPS.Region_id, COUNT(*) AS Number
FROM (
		SELECT DISTINCT R2.Position_id, R2.Sender_id
		FROM PositionReport R2
			INNER JOIN (
				SELECT Sender_id, MAX(Report_time) AS Report_time
				FROM PositionReport
				WHERE Report_time BETWEEN @startTime AND @endTime
				GROUP BY Sender_id
			) AS CP /*Current Position*/ ON R2.Report_time = CP.Report_time AND R2.Sender_id = CP.Sender_id
	) AS R
	LEFT JOIN RegionPositionSet RPS ON R.Position_id = RPS.Position_id
GROUP BY RPS.Region_id

INSERT INTO @regionCount SELECT 1, COUNT(*) FROM @regionCount WHERE Region_id != 0

/*结果*/
SELECT ISNULL(R.Region_name, '(未定义)') AS Name,
	ISNULL(C.Region_id, -1) AS Id,
	C.Number
FROM @regionCount C
	LEFT JOIN Region R ON R.Region_id = C.Region_id
ORDER BY Id
