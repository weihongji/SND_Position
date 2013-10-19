IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPeopleSearch]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPeopleSearch]
END
GO

CREATE PROCEDURE [dbo].[spPeopleSearch] (
	@senderId int = NULL,
	@lampId varchar(8) = NULL,
	@peopleName varchar(32) = NULL,
	@peopleId int = NULL,
	@deptId int = NULL,
	@rankId int = NULL,
	@forTime datetime = NULL,
	@isInWell bit = NULL
)
AS

DECLARE @startTime datetime, @endTime datetime
DECLARE @peopleSenderIDs AS TABLE (PeopleId int, SenderId int)
DECLARE @onGroundPositionIDs AS TABLE (PositionId smallint PRIMARY KEY)

DECLARE @debug int
SET @debug = 0 /* A value larger than 0 indicates to output debugging information */

IF @debug = 1 BEGIN
	--SET @senderId = 1035 /* The corresponding People_Id is 80003 */
	SET @peopleId = 527 /* The corresponding Sender_Id is 2760 */
END
IF @debug = 2 BEGIN
	SET @forTime = '2013-08-14 10:00'
END

/* Formalize Parameters */
IF @senderId < 0 SET @senderId = NULL
IF @peopleId < 0 SET @peopleId = NULL
IF @deptId < 0 SET @deptId = NULL
IF @rankId < 0 SET @rankId = NULL
IF LEN(@lampId) = 0 SET @lampId = NULL
IF LEN(@peopleName) = 0 SET @peopleName = NULL
IF @forTime IS NULL SET @forTime = GETDATE()

/* Populate table @peopleSenderIDs */
IF @senderId IS NOT NULL OR LEN(@lampId) > 0 BEGIN /* Sender IDs */
	IF @senderId IS NOT NULL BEGIN
		INSERT @peopleSenderIDs(SenderId) SELECT @senderId
	END
	ELSE IF LEN(@lampId) > 0 BEGIN
		INSERT INTO @peopleSenderIDs(SenderId) SELECT Sender_id FROM Lamp WHERE Lamp_id = @lampId
	END
	UPDATE @peopleSenderIDs
	SET PeopleId = (
			SELECT TOP 1 PS2.People_id FROM (
					SELECT Sender_id, MAX(First_use_time) AS First_use_time FROM PeopleSender
					WHERE Sender_id = SenderId
					GROUP BY Sender_id
				) AS PS1
				INNER JOIN PeopleSender PS2 ON PS1.Sender_id = PS2.Sender_id AND PS1.First_use_time = PS2.First_use_time
		)
END
ELSE BEGIN /* People IDs */
	IF @peopleId IS NOT NULL BEGIN
		INSERT INTO @peopleSenderIDs(PeopleId) SELECT @peopleId
	END
	ELSE BEGIN
		INSERT INTO @peopleSenderIDs(PeopleId)
		SELECT People_id FROM People
		WHERE (@peopleName IS NULL OR People_name = @peopleName)
			AND (@deptId IS NULL OR Dept_id = @deptId)
			AND (@rankId IS NULL OR Rank_id = @rankId)
	END
	UPDATE @peopleSenderIDs
	SET SenderId = (
			SELECT TOP 1 PS2.Sender_id FROM (
					SELECT People_id, MAX(First_use_time) AS First_use_time FROM PeopleSender
					WHERE People_id = PeopleId
					GROUP BY People_id
				) AS PS1
				INNER JOIN PeopleSender PS2 ON PS1.People_id = PS2.People_id AND PS1.First_use_time = PS2.First_use_time
		)
END

/* Populate table @onGroundPositionIDs */
IF @isInWell = 0 BEGIN /*¾®ÉÏ*/
	INSERT INTO @onGroundPositionIDs
	SELECT Position_id FROM RegionPositionSet WHERE Region_id = 0
END
ELSE IF @isInWell = 1 BEGIN /*¾®ÏÂ*/
	INSERT INTO @onGroundPositionIDs
	SELECT Position_id FROM RegionPositionSet WHERE Region_id != 0
END

IF @debug = 1 BEGIN
	SELECT * FROM @peopleSenderIDs
END

/* Date & Time criteria */
SET @startTime = DATEADD(MINUTE, -10, @forTime)
SET @endTime = @forTime

/* Final Result */
SELECT P.People_id AS PeopleId, P.People_name AS PeopleName,
	CASE WHEN P.Gender = 1 THEN N'ÄÐ' ELSE N'Å®' END AS Gender,
	D.Dept_name AS DepartmentName,
	WT.Worktype_name AS WorkType, RK.Rank_name AS [Rank], PS.SenderId,
	R.Position_id AS PositionId, R.Position_desc AS PositionName
FROM People P
	INNER JOIN Department D ON P.Dept_id = D.Dept_id
	INNER JOIN WorkType WT ON P.Worktype_id = WT.Worktype_id
	INNER JOIN [Rank] RK ON P.Rank_id = RK.Rank_id
	INNER JOIN @peopleSenderIDs PS ON P.People_id = PS.PeopleId
	LEFT JOIN (
		SELECT R1.*, P1.Position_desc
		FROM (
				SELECT DISTINCT R2.*
				FROM PositionReport R2
					INNER JOIN (
						SELECT Sender_id, MAX(Report_time) AS Report_time FROM PositionReport
						WHERE Report_time BETWEEN @startTime AND @endTime AND Sender_id IN (SELECT SenderId FROM @peopleSenderIDs)
						GROUP BY Sender_id
					) AS CP /*Current Position*/ ON R2.Report_time = CP.Report_time AND R2.Sender_id = CP.Sender_id

			) AS R1
			LEFT JOIN Position P1 ON R1.Position_id = P1.Position_id
	) AS R ON PS.SenderId = R.Sender_id
WHERE P.People_id IN (SELECT PeopleId FROM @peopleSenderIDs)
	AND (
		@isInWell IS NULL
		OR Position_id IN (SELECT * FROM @onGroundPositionIDs)
	)
