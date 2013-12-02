using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long CompanyMineId { get; set; }
        public string Information { get; set; }
        public string Remark { get; set; }
    }
}
