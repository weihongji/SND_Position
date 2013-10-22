using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AlarmReportCriteria
    {
        public byte? Type;
        public int? Param1;
        public int? Param2;
        public int? ProcessStatus;
        public DateTime? StartAt;
        public DateTime? EndAt;
    }
}
