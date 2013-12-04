using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorMap
    {
        public int Id { get; set; }
        public int MonitorSystemId { get; set; }
        public string Name { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int SizeType { get; set; }
        public string DisplayName { get; set; }
    }
}
