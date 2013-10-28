--spQueryAlarmListCurrent 255, -1, -1, 255, '2013-08-14', '2013-08-14 23:59:59'

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spQueryAlarmListCurrent]') AND type in (N'P', N'PC')) BEGIN
	DROP PROCEDURE [dbo].[spQueryAlarmListCurrent]
END
GO

CREATE PROCEDURE dbo.spQueryAlarmListCurrent
	@Alarm_type TINYINT = 255,		-- 警报类型 255: 所有类型；0: 区域报警；10: 分站报警；20: 收发器报警；30:发射器报警
	@Alarm_param1 INT = -1,			-- 警报参数1  -1：所有参数
	@Alarm_param2 INT = -1,			-- 警报参数2  -1：所有参数
	@Process_status TINYINT = 255,	-- 处理状态 255:所有状态；0: 尚未恢复； 1: 自动恢复； 2: 手动恢复
	@Begin_time DATETIME = NULL,	-- 起始时间
	@End_time DATETIME = NULL		-- 终止时间
AS
  DECLARE @Alarm_id            AS INT
  DECLARE @First_report_time   AS DATETIME
  DECLARE @Last_report_time    AS DATETIME
  DECLARE @Process_time        AS DATETIME
  DECLARE @Alarm_level         AS TINYINT
  DECLARE @Alarm_type_info     AS VARCHAR(32)
  DECLARE @Alarm_param_info    AS VARCHAR(128)
  DECLARE @Process_status_info AS VARCHAR(12)
  DECLARE @Login_name          AS VARCHAR(32)

  DECLARE @Region_name         AS VARCHAR(32)
  DECLARE @People_name         AS VARCHAR(32)
  DECLARE @Worktype_name       AS VARCHAR(32)

  DECLARE @Position_id         AS SMALLINT
  DECLARE @Position_desc       AS VARCHAR(64)

  DECLARE @AlarmList TABLE  ( Alarm_id            INT,
                              Alarm_type          TINYINT,
                              Alarm_type_info     VARCHAR(32),
                              Alarm_param1        INT,
                              Alarm_param2        INT,
                              Alarm_param_info    VARCHAR(128),
                              Alarm_level         TINYINT,
                              First_report_time   DATETIME,
                              Last_report_time    DATETIME,
                              Process_time        DATETIME,
                              Process_status      TINYINT,
                              Process_status_info VARCHAR(12),
                              Login_name          VARCHAR(32) )


  IF @Begin_time IS NULL OR @End_time IS NULL BEGIN
    SET @End_time = GETDATE()
    SET @Begin_time = DATEADD(MINUTE, -10, @End_time)
  END
  
  DECLARE CursorAlarmList CURSOR FAST_FORWARD FOR			-- 选择合适记录
    SELECT Alarm_id, Alarm_type, Alarm_param1, Alarm_param2, First_report_time, Last_report_time, Process_time, Process_status, Login_name
    FROM CurrentAlarmReport
    WHERE ( @Alarm_type=255 						-- -1: 所有类型
            OR @Alarm_type=0  AND Alarm_type>=1  AND Alarm_type<=9	--  0：区域报警
            OR @Alarm_type=10 AND Alarm_type>=11 AND Alarm_type<=19	-- 10：分站报警
            OR @Alarm_type=20 AND Alarm_type>=21 AND Alarm_type<=29	-- 20：收发器报警
            OR @Alarm_type=30 AND Alarm_type>=31 AND Alarm_type<=39	-- 30：发射器报警
            OR @Alarm_type = Alarm_type	)				-- 指定报警
          AND
          ( @Alarm_param1 = -1						-- -1: 所有参数1
            OR @Alarm_param1 = Alarm_param1 )                           -- 指定参数1
          AND
          ( @Alarm_param2 = -1						-- -1: 所有参数2
            OR @Alarm_param2 = Alarm_param2 )                           -- 指定参数2
          AND
          ( @Process_status = 255
            OR @Process_status=Process_status )                         -- 指定处理状态
          AND
          ( @Begin_time<=First_report_time AND First_report_time<=@End_time
            OR @Begin_time<=Last_report_time AND Last_report_time<=@End_time )
    ORDER BY Last_report_time DESC

  OPEN CursorAlarmList
  FETCH NEXT FROM CursorAlarmList INTO @Alarm_id,@Alarm_type,@Alarm_param1,@Alarm_param2,@First_report_time,@Last_report_time,@Process_time,@Process_status,@Login_name
  WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @Alarm_level = 3
        SET @Alarm_type_info = ''
        SELECT TOP 1 @Alarm_type_info = Alarm_name, @Alarm_level = Alarm_level
        FROM AlarmType
        WHERE Alarm_type = @Alarm_type
 
      SET @Region_name = ''
      IF @Alarm_type <10
        SELECT @Region_name = Region_name
        FROM Region
        WHERE Region_id = @Alarm_param1

      SET @People_name = dbo.sfSenderToPeopleName( @First_report_time, @Alarm_param2 )

      SET @Worktype_name = ''
      IF ( @Alarm_type = 4 ) OR ( @Alarm_type = 6 )
        SELECT @Worktype_name = Worktype_name
        FROM WorkType
        WHERE Worktype_id = @Alarm_param2

      SET @Position_id = -1
      SET @Position_desc = '无'
      IF @Alarm_type = 32					-- 报警地点
        BEGIN
          SELECT TOP 1 @Position_id = a.Position_id
          FROM dbo.RegionReport AS a
          WHERE ( a.Sender_id = @Alarm_param2 )
                AND
                ( a.First_report_time <= @Last_report_time OR a.Last_report_time <= @Last_report_time )
          ORDER BY a.Last_report_time DESC
      
          IF @Position_id != -1
             SELECT TOP 1 @Position_desc = a.Position_desc
             FROM dbo.Position AS a
             WHERE a.Position_id = @Position_id
        END

      SET @Alarm_param_info =				-- 警报参数
          CASE
            WHEN @Alarm_type=1  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
            WHEN @Alarm_type=2  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
                                     +'；'+'发射器号:'+LTRIM(STR(@Alarm_param2))+'；'+'姓名:'+LTRIM(RTRIM(@People_name))
            WHEN @Alarm_type=3  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
                                     +'；'+'发射器号:'+LTRIM(STR(@Alarm_param2))+'；'+'姓名:'+LTRIM(RTRIM(@People_name))
            WHEN @Alarm_type=4  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
                                     +'；'+'工种:'+LTRIM(RTRIM(@Worktype_name))
            WHEN @Alarm_type=5  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
                                     +'；'+'发射器号:'+LTRIM(STR(@Alarm_param2))+'；'+'姓名:'+LTRIM(RTRIM(@People_name))
            WHEN @Alarm_type=6  THEN '区域编号:'+LTRIM(STR(@Alarm_param1))+'；'+'区域名称:'+LTRIM(RTRIM(@Region_name))
                                     +'；'+'工种:'+LTRIM(RTRIM(@Worktype_name))
            WHEN @Alarm_type=11 THEN '分站号:'+LTRIM(STR(@Alarm_param1))
            WHEN @Alarm_type=12 THEN '分站号:'+LTRIM(STR(@Alarm_param1))
            WHEN @Alarm_type=13 THEN '分站号:'+LTRIM(STR(@Alarm_param1))
            WHEN @Alarm_type=14 THEN '分站号:'+LTRIM(STR(@Alarm_param1))
            WHEN @Alarm_type=15 THEN '分站号:'+LTRIM(STR(@Alarm_param1))
            WHEN @Alarm_type=21 THEN '分站号:'+LTRIM(STR(@Alarm_param1))+'；'+'收发器号:'+LTRIM(STR(@Alarm_param2))
            WHEN @Alarm_type=22 THEN '分站号:'+LTRIM(STR(@Alarm_param1))+'；'+'收发器号:'+LTRIM(STR(@Alarm_param2))
            WHEN @Alarm_type=23 THEN '分站号:'+LTRIM(STR(@Alarm_param1))+'；'+'收发器号:'+LTRIM(STR(@Alarm_param2))
            WHEN @Alarm_type=24 THEN '分站号:'+LTRIM(STR(@Alarm_param1))+'；'+'收发器号:'+LTRIM(STR(@Alarm_param2))
            WHEN @Alarm_type=31 THEN '发射器号:'+LTRIM(STR(@Alarm_param2))+'；'+'人员姓名:'+LTRIM(RTRIM(@People_name))
            WHEN @Alarm_type=32 THEN '发射器号:'+LTRIM(STR(@Alarm_param2))+'；'+'人员姓名:'+LTRIM(RTRIM(@People_name))+'；'+'报警位置:'+LTRIM(RTRIM(@Position_desc))
	    WHEN @Alarm_type=33 THEN '发射器号:'+LTRIM(STR(@Alarm_param2))
            ELSE '其他'
          END

      SET @Process_status_info =
          CASE
            WHEN @Process_status=0  THEN '尚未恢复'
            WHEN @Process_status=1  THEN '自动恢复'
            WHEN @Process_status=2  THEN '手动恢复'
            ELSE '其他'+LTRIM(STR(@Process_status))
          END
      INSERT @AlarmList(Alarm_id,
                        Alarm_type,
                        Alarm_type_info,
                        Alarm_param1,
                        Alarm_param2,
                        Alarm_param_info,
                        Alarm_level,
                        First_report_time,
                        Last_report_time,
                        Process_time,
                        Process_status,
                        Process_status_info,
                        Login_name )
      	     	VALUES( @Alarm_id,
                        @Alarm_type,
                        @Alarm_type_info,
                        @Alarm_param1,
                        @Alarm_param2,
                        @Alarm_param_info,
                        @Alarm_level,
                        @First_report_time,
                        @Last_report_time,
                        @Process_time,
                        @Process_status,
                        @Process_status_info,
                        @Login_name )
      FETCH NEXT FROM CursorAlarmList INTO @Alarm_id,@Alarm_type,@Alarm_param1,@Alarm_param2,@First_report_time,@Last_report_time,@Process_time, @Process_status, @Login_name
    END

  CLOSE CursorAlarmList
  DEALLOCATE CursorAlarmList

  SELECT Alarm_id            AS 'Id',
         Alarm_type          AS 'Type',
         Alarm_type_info     AS 'TypeInfo',
         Alarm_param1        AS 'Param1',
         Alarm_param2        AS 'Param2',
         Alarm_param_info    AS 'ParamInfo',
         Alarm_level         AS 'Level',
         CONVERT(VARCHAR(20),First_report_time,20)   AS 'StartAt',
         CONVERT(VARCHAR(20),Last_report_time, 20)   AS 'EndAt',
         CASE 
           WHEN Process_time < '1990/01/01' THEN SPACE(20)
           ELSE CONVERT(VARCHAR(20),Process_time,     20)
         END                                         AS 'ProcessAt',
         Process_status      AS 'Status',
         Process_status_info AS 'StatusInfo',
         Login_name          AS 'Operator'
  FROM @AlarmList

END_LOC:
  RETURN 0
