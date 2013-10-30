IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spCopyDataPrepare]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spCopyDataPrepare]
END
GO

CREATE PROCEDURE [dbo].[spCopyDataPrepare]
AS
BEGIN TRAN
	/* DROP foreign keys */
	ALTER TABLE Receiver DROP FK_Receiver_Branch
	ALTER TABLE Branch DROP FK_Branch_Position
	ALTER TABLE Branch DROP FK_Branch_Product
	ALTER TABLE People DROP FK_People_Department
	ALTER TABLE Lamp DROP FK_Lamp_Sender
	ALTER TABLE PeopleSender DROP FK_PeopleSender_People
	ALTER TABLE People DROP FK_People_Rank
	ALTER TABLE People DROP FK_People_WorkType
	ALTER TABLE Receiver DROP FK_Receiver_Position
	ALTER TABLE RegionPositionSet DROP FK_RegionPositionSet_Position
	ALTER TABLE PositionReport DROP FK_PositionReport_Position
	ALTER TABLE CurrentPositionReport DROP FK_CurrentPositionReport_Position
	ALTER TABLE Sender DROP FK_Sender_Product
	ALTER TABLE Receiver DROP FK_Receiver_Product
	ALTER TABLE RegionPositionSet DROP FK_RegionPositionSet_Region
	ALTER TABLE PeopleSender DROP FK_PeopleSender_Sender
COMMIT
