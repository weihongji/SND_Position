using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorContent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int MonitorSystemId { get; set; }
        public string Image { get; set; }
        public string ImageOverview { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
    }
}
