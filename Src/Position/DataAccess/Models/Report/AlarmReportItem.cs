using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class AlarmReportItem
	{
		public int Id { get; set; }
        public byte Type { get; set; }
        public string TypeInfo { get; set; }
        public int Param1 { get; set; }
        public int Param2 { get; set; }
        public string ParamInfo { get; set; }
        public byte Level { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public string ProcessAt { get; set; }
        public byte Status { get; set; }
        public string StatusInfo { get; set; }
        public string Operator { get; set; }
    }
}
