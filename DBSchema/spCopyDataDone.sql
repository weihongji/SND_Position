IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spCopyDataDone]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spCopyDataDone]
END
GO

CREATE PROCEDURE [dbo].[spCopyDataDone]
AS
BEGIN TRAN
	/* ADD dropped foreign keys back */
	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD CONSTRAINT FK_Receiver_Branch FOREIGN KEY([Branch_id]) REFERENCES [dbo].[Branch] ([Branch_id])
	ALTER TABLE [dbo].[Branch]  WITH CHECK ADD CONSTRAINT FK_Branch_Position FOREIGN KEY([Position_id]) REFERENCES [dbo].[Position] ([Position_id])
	ALTER TABLE [dbo].[Branch]  WITH CHECK ADD CONSTRAINT FK_Branch_Product FOREIGN KEY([Product_id]) REFERENCES [dbo].[Product] ([Product_id])
	ALTER TABLE [dbo].[People]  WITH CHECK ADD CONSTRAINT FK_People_Department FOREIGN KEY([Dept_id]) REFERENCES [dbo].[Department] ([Dept_id])
	ALTER TABLE [dbo].[Lamp]  WITH CHECK ADD CONSTRAINT FK_Lamp_Sender FOREIGN KEY([Sender_id]) REFERENCES [dbo].[Sender] ([Sender_id])
	ALTER TABLE [dbo].[PeopleSender]  WITH CHECK ADD CONSTRAINT FK_PeopleSender_People FOREIGN KEY([People_id]) REFERENCES [dbo].[People] ([People_id])
	ALTER TABLE [dbo].[People]  WITH CHECK ADD CONSTRAINT FK_People_Rank FOREIGN KEY([Rank_id]) REFERENCES [dbo].[Rank] ([Rank_id])
	ALTER TABLE [dbo].[People]  WITH CHECK ADD CONSTRAINT FK_People_WorkType FOREIGN KEY([Worktype_id]) REFERENCES [dbo].[WorkType] ([Worktype_id])
	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD CONSTRAINT FK_Receiver_Position FOREIGN KEY([Position_id]) REFERENCES [dbo].[Position] ([Position_id])
	ALTER TABLE [dbo].[RegionPositionSet]  WITH CHECK ADD CONSTRAINT FK_RegionPositionSet_Position FOREIGN KEY([Position_id]) REFERENCES [dbo].[Position] ([Position_id])
	ALTER TABLE [dbo].[PositionReport]  WITH CHECK ADD CONSTRAINT FK_PositionReport_Position FOREIGN KEY([Position_id]) REFERENCES [dbo].[Position] ([Position_id])
	ALTER TABLE [dbo].[CurrentPositionReport]  WITH CHECK ADD CONSTRAINT FK_CurrentPositionReport_Position FOREIGN KEY([Position_id]) REFERENCES [dbo].[Position] ([Position_id])
	ALTER TABLE [dbo].[Sender]  WITH CHECK ADD CONSTRAINT FK_Sender_Product FOREIGN KEY([Product_id]) REFERENCES [dbo].[Product] ([Product_id])
	ALTER TABLE [dbo].[Receiver]  WITH CHECK ADD CONSTRAINT FK_Receiver_Product FOREIGN KEY([Product_id]) REFERENCES [dbo].[Product] ([Product_id])
	ALTER TABLE [dbo].[RegionPositionSet]  WITH CHECK ADD CONSTRAINT FK_RegionPositionSet_Region FOREIGN KEY([Region_id]) REFERENCES [dbo].[Region] ([Region_id])
	ALTER TABLE [dbo].[PeopleSender]  WITH CHECK ADD CONSTRAINT FK_PeopleSender_Sender FOREIGN KEY([Sender_id]) REFERENCES [dbo].[Sender] ([Sender_id])
COMMIT