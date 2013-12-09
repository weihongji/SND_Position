using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class MonitorMapModel
    {
        public MonitorSystem System { get; set; }
        public List<MonitorMap> Maps { get; set; }
        public List<string> MapNames { get; set; }

        public MonitorMapModel() {
            Maps = new List<MonitorMap>();
            MapNames = new List<string>();
        }
    }
}