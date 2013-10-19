using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Region
    {
        public short Region_id { get; set; }
        public string Region_name { get; set; }
        public byte Region_type { get; set; }
        public short People_max { get; set; }
        public short Linger_max { get; set; }
        public short Status { get; set; }
        public int Display_x { get; set; }
        public int Display_y { get; set; }
        public byte Display_type { get; set; }
        public string Region_info { get; set; }
    }
}
