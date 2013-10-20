IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPeopleCountByRank]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPeopleCountByRank]
END
GO

CREATE PROCEDURE [dbo].[spPeopleCountByRank] (
	@forTime datetime = NULL
)
AS

--SET @forTime = '2013-08-14 10:00'

DECLARE @startTime datetime, @endTime datetime
DECLARE @positionIDs AS TABLE (PositionId smallint PRIMARY KEY)
DECLARE @groundPeopleCount AS TABLE (Rank_id smallint, Number int)
DECLARE @wellPeopleCount AS TABLE (Rank_id smallint, Number int)

/* Date & Time criteria */
SET @startTime = DATEADD(MINUTE, -10, @forTime)
SET @endTime = @forTime

/*����*/
DELETE FROM @positionIDs
INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id = 0

INSERT INTO @groundPeopleCount
SELECT P.Rank_id, COUNT(*) AS Number
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
GROUP BY P.Rank_id


/*����*/
DELETE FROM @positionIDs
INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id != 0

INSERT INTO @wellPeopleCount
SELECT P.Rank_id, COUNT(*) AS Number
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
GROUP BY P.Rank_id

/*���*/
SELECT ISNULL(M.Rank_name, '(δ����)') AS Name,
	ISNULL(M.Rank_id, -1) AS Id,
	ISNULL(M.Number, 0) AS Registration,
	ISNULL(G.Number, 0) AS Ground,
	ISNULL(W.Number, 0) AS Well
FROM (
		SELECT R.Rank_name, R.Rank_id, P.Number
		FROM (
				SELECT Rank_id, COUNT(*) AS Number FROM People
				GROUP BY Rank_id
			) AS P
			INNER JOIN [Rank] R ON P.Rank_id = R.Rank_id
	) AS M
	FULL JOIN @groundPeopleCount G ON M.Rank_id = G.Rank_id
	FULL JOIN @wellPeopleCount AS W ON M.Rank_id = W.Rank_id