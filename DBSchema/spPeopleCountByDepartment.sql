﻿IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPeopleCountByDepartment]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPeopleCountByDepartment]
END
GO

CREATE PROCEDURE [dbo].[spPeopleCountByDepartment] (
	@forTime datetime = NULL
)
AS

--SET @forTime = '2013-08-14 10:00'

DECLARE @startTime datetime, @endTime datetime
DECLARE @positionIDs AS TABLE (PositionId smallint PRIMARY KEY)
DECLARE @groundPeopleCount AS TABLE (Dept_id smallint, Number int)
DECLARE @wellPeopleCount AS TABLE (Dept_id smallint, Number int)

/* Date & Time criteria */
SET @startTime = DATEADD(MINUTE, -10, @forTime)
SET @endTime = @forTime

/*井上*/
DELETE FROM @positionIDs
INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id = 0

INSERT INTO @groundPeopleCount
SELECT P.Dept_id, COUNT(*) AS Number
FROM (
		SELECT DISTINCT R2.Position_id, R2.Sender_id
		FROM PositionReport R2
			INNER JOIN (
				SELECT Sender_id, MAX(Report_time) AS Report_time
				FROM PositionReport
				WHERE Report_time BETWEEN @startTime AND @endTime
					AND Position_id IN (SELECT PositionId FROM @positionIDs)
				GROUP BY Sender_id
			) AS CP /*Current Position*/ ON R2.Report_time = CP.Report_time AND R2.Sender_id = CP.Sender_id
	) AS R
	LEFT JOIN PeopleSender PS ON R.Sender_id = PS.Sender_id
	LEFT JOIN People P ON PS.People_id = P.People_id
GROUP BY P.Dept_id


/*井下*/
DELETE FROM @positionIDs
INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id != 0

INSERT INTO @wellPeopleCount
SELECT P.Dept_id, COUNT(*) AS Number
FROM (
		SELECT DISTINCT R2.Position_id, R2.Sender_id
		FROM PositionReport R2
			INNER JOIN (
				SELECT Sender_id, MAX(Report_time) AS Report_time
				FROM PositionReport
				WHERE Report_time BETWEEN @startTime AND @endTime
					AND Position_id IN (SELECT PositionId FROM @positionIDs)
				GROUP BY Sender_id
			) AS CP /*Current Position*/ ON R2.Report_time = CP.Report_time AND R2.Sender_id = CP.Sender_id
	) AS R
	LEFT JOIN PeopleSender PS ON R.Sender_id = PS.Sender_id
	LEFT JOIN People P ON PS.People_id = P.People_id
GROUP BY P.Dept_id

/*结果*/
SELECT ISNULL(M.Dept_name, '(未定义)') AS Name,
	ISNULL(M.Dept_id, -1) AS Id,
	ISNULL(M.Number, 0) AS Registration,
	ISNULL(G.Number, 0) AS Ground,
	ISNULL(W.Number, 0) AS Well
FROM (
		SELECT D.Dept_name, D.Dept_id, P.Number
		FROM (
				SELECT Dept_id, COUNT(*) AS Number FROM People
				GROUP BY Dept_id
			) AS P
			INNER JOIN Department D ON P.Dept_id = D.Dept_id
	) AS M
	FULL JOIN @groundPeopleCount G ON M.Dept_id = G.Dept_id
	FULL JOIN @wellPeopleCount AS W ON M.Dept_id = W.Dept_id