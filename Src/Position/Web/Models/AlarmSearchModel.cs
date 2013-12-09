using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Models
{
    public class AlarmSearchModel
    {
        public List<AlarmReportItem> ReportItems { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Receivers { get; set; }
        public List<SelectListItem> Status { get; set; }

        public AlarmSearchModel() {
            ReportItems = new List<AlarmReportItem>();
            Regions = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
            Receivers = new List<SelectListItem>();
            Status = new List<SelectListItem>();
            Status.Add(new SelectListItem() { Value = "255", Text = "所有状态", Selected = true });
            Status.Add(new SelectListItem() { Value = "0",  Text = "尚未恢复"});
            Status.Add(new SelectListItem() { Value = "1", Text = "自动恢复" });
            Status.Add(new SelectListItem() { Value = "2", Text = "手动恢复" });
        }
    }
}