using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PeopleSearchCriteria
    {
        public int? SenderId;
        public string LampId;
        public string PersonName;
        public int? PersonId;
        public int? DeptId;
        public int? RankId;
        public DateTime? ReportForTime;
        public WorkPlace WorkPlace;
    }
}
