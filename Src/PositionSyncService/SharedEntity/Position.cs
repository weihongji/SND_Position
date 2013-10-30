using System;
using System.Collections.Generic;

namespace SharedEntity
{
    public partial class Position
    {
        public short Position_id { get; set; }
        public int Position_x { get; set; }
        public int Position_y { get; set; }
        public int Position_z { get; set; }
        public double Position_sin { get; set; }
        public double Position_cos { get; set; }
        public double Position_vcos { get; set; }
        public string Position_desc { get; set; }
    }
}
