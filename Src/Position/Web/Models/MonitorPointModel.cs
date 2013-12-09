using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class MonitorPointModel
    {
        public MonitorSystem System { get; set; }
        public MonitorMap Map { get; set; }
        public List<MonitorContent> ContentList { get; set; }
        public List<MonitorPoint> MonitorList { get; set; }

        public MonitorPointModel() {
            ContentList = new List<MonitorContent>();
            MonitorList = new List<MonitorPoint>();
        }
    }
}