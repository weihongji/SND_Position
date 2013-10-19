using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PositionSearchCriteria
    {
        public int? RegionId;
        public int? BranchId;
        public int? ReceiverId;
        public int? PositionId;
        public DateTime? ReportForTime;
    }
}
