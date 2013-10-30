IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLog]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spLog]
END
GO

CREATE PROCEDURE dbo.spLog
	@Message varchar(255),
	@Logger varchar(50) = NULL,
	@Level varchar(20) = 'INFO',		-- INFO, WARNING, ERROR, DEBUG
	@newId int = NULL OUTPUT
AS

IF @Level IS NULL SET @Level = 'INFO'

INSERT INTO PositionSyncLog ([Level], [Message], Logger)
VALUES (@Level, @Message, @Logger)

SET @newId = SCOPE_IDENTITY()