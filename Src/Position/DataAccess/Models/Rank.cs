using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Rank
    {
        public short Rank_id { get; set; }
        public byte Rank_type { get; set; }
        public string Rank_name { get; set; }
    }
}
