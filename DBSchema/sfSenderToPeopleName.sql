IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sfSenderToPeopleName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) BEGIN
	DROP FUNCTION [dbo].[sfSenderToPeopleName]
END
GO

CREATE FUNCTION dbo.sfSenderToPeopleName(@time datetime, @senderId int)
RETURNS varchar(32)
BEGIN
	DECLARE @name varchar(32)

	SELECT @name = P.People_name
	FROM People P INNER JOIN PeopleSender PS ON P.People_id = PS.People_id
	WHERE PS.First_use_time < @time AND @time < PS.Last_use_time
		AND PS.Sender_id = @senderId

	RETURN @name;
END