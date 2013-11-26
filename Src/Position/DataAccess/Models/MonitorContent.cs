using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorContent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }
}
