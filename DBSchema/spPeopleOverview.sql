IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPeopleOverview]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spPeopleOverview]
END
GO

CREATE PROCEDURE [dbo].[spPeopleOverview] (
	@senderId int = NULL,
	@lampId varchar(8) = NULL,
	@peopleName varchar(32) = NULL,
	@peopleId int = NULL,
	@deptId int = NULL,
	@rankId int = NULL,
	@forTime datetime = NULL
)
AS

SELECT P.People_id AS PeopleId, P.People_name AS PeopleName, D.Dept_name AS DepartmentName,
	WT.Worktype_name AS WorkType, R.Rank_name AS [Rank],
	SL.Lamp_id AS LampId, SL.Sender_id AS SenderId,
	CASE WHEN SL.Status = 1 THEN N'∆Ù”√' ELSE N'Ω˚”√' END AS SenderStatus
FROM People P
	INNER JOIN Department D ON P.Dept_id = D.Dept_id
	INNER JOIN WorkType WT ON P.Worktype_id = WT.Worktype_id
	INNER JOIN [Rank] R ON P.Rank_id = R.Rank_id
	LEFT JOIN (
		SELECT PS.People_id, PS.Sender_id, L.Lamp_id, S.Status
		FROM (SELECT People_id, MAX(Sender_id) AS Sender_id FROM PeopleSender GROUP BY People_id) AS PS
			INNER JOIN Sender S ON S.Sender_id = PS.Sender_id
			LEFT JOIN Lamp L ON PS.Sender_id = L.Sender_id
	) AS SL ON P.People_id = SL.People_id