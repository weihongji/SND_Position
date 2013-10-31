using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class AlarmType
    {
        public byte Alarm_type { get; set; }
        public string Alarm_name { get; set; }
        public string Param1_name { get; set; }
        public string Param2_name { get; set; }
        public byte Alarm_level { get; set; }
        public byte Alarm_attrib { get; set; }
        public int Valid_seconds { get; set; }
        public int Auto_recovery_seconds { get; set; }
    }
}
