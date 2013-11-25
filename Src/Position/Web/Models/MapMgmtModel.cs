using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MapMgmtModel
    {
        public DataAccess.Models.Map Map { get; set; }
        public List<MonitorPoint> MonitorList { get; set; }

        public MapMgmtModel() {
            MonitorList = new List<MonitorPoint>();
        }
    }
}