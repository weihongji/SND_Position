using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Scale { get; set; }
        public DateTime DTStamp { get; set; }
    }
}
