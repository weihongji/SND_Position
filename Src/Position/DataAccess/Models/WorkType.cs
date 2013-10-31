using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class WorkType
    {
        public short Worktype_id { get; set; }
        public byte Worktype_type { get; set; }
        public string Worktype_name { get; set; }
    }
}
