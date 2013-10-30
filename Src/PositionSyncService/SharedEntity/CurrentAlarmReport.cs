using System;
using System.Collections.Generic;

namespace SharedEntity
{
    public partial class CurrentAlarmReport
    {
        public int Alarm_id { get; set; }
        public byte Alarm_type { get; set; }
        public int Alarm_param1 { get; set; }
        public int Alarm_param2 { get; set; }
        public System.DateTime First_report_time { get; set; }
        public System.DateTime Last_report_time { get; set; }
        public System.DateTime Process_time { get; set; }
        public string Login_name { get; set; }
        public byte Process_status { get; set; }
    }
}
