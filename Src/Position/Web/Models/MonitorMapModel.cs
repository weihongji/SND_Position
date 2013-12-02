using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MonitorMapModel
    {
        public MonitorSystem System { get; set; }
        public MonitorMap SmallMap { get; set; }
        public MonitorMap MedimMap { get; set; }
        public MonitorMap LargeMap { get; set; }
    }
}