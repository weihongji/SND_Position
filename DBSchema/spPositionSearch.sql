IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPositionSearch]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPositionSearch]
END
GO

CREATE PROCEDURE [dbo].[spPositionSearch] (
	@regionId int = NULL,
	@branchId int = NULL,
	@receiverId int = NULL,
	@positionId int = NULL,
	@forTime datetime = NULL
)
AS

DECLARE @startTime datetime, @endTime datetime
DECLARE @peopleSenderIDs AS TABLE (PeopleId int, SenderId int)
DECLARE @onGroundPositionIDs AS TABLE (PositionId smallint PRIMARY KEY)
DECLARE @positionIDs AS TABLE (PositionId smallint PRIMARY KEY)

--SET @forTime = '2013-08-14 10:00'

IF @regionId IS NULL BEGIN
	IF @positionId IS NOT NULL BEGIN
		INSERT INTO @positionIDs SELECT @positionId
	END
	ELSE BEGIN
		INSERT INTO @positionIDs SELECT Position_id FROM Position
	END
END
ELSE IF @regionId = 1 BEGIN /*¾®ÏÂ*/
	INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id != 0
END
ELSE BEGIN
	INSERT INTO @positionIDs SELECT Position_id FROM RegionPositionSet WHERE Region_id = @regionId
END


/* Formalize Parameters */
IF @regionId < 0 SET @regionId = NULL
IF @forTime IS NULL SET @forTime = GETDATE()

/* Date & Time criteria */
SET @startTime = DATEADD(MINUTE, -10, @forTime)
SET @endTime = @forTime

/* Final Result */
SELECT P.People_id AS PeopleId, P.People_name AS PeopleName,
	CASE WHEN P.Gender = 1 THEN N'ÄÐ' ELSE N'Å®' END AS Gender,
	D.Dept_name AS DepartmentName,
	WT.Worktype_name AS WorkType, RK.Rank_name AS [Rank], R.Sender_id AS SenderId,
	R.Position_id AS PositionId, Position.Position_desc AS PositionName
FROM (
		SELECT DISTINCT R2.Position_id, R2.Sender_id
		FROM PositionReport R2
			INNER JOIN (
				SELECT Sender_id, MAX(Report_time) AS Report_time
				FROM PositionReport
				WHERE Report_time BETWEEN @startTime AND @endTime
					AND Position_id IN (SELECT PositionId FROM @positionIDs)
					AND (@branchId IS NULL OR Branch_id = @branchId)
					AND (@receiverId IS NULL OR Receiver_id = @receiverId)
				GROUP BY Sender_id
			) AS CP /*Current Position*/ ON R2.Report_time = CP.Report_time AND R2.Sender_id = CP.Sender_id
	) AS R
	INNER JOIN Position ON Position.Position_id = R.Position_id
	LEFT JOIN PeopleSender PS ON R.Sender_id = PS.Sender_id
	LEFT JOIN People P ON PS.People_id = P.People_id
	LEFT JOIN Department D ON P.Dept_id = D.Dept_id
	LEFT JOIN WorkType WT ON P.Worktype_id = WT.Worktype_id
	LEFT JOIN [Rank] RK ON P.Rank_id = RK.Rank_id
