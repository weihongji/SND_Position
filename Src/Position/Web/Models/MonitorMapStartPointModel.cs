﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MonitorMapStartPointModel
    {
        public MonitorMap Map { get; set; }
        public MonitorContent Pin { get; set; }
    }
}