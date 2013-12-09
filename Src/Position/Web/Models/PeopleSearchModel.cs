using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Models
{
    public class PeopleSearchModel
    {
        public List<PeopleSearchReportItem> ReportItems { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Ranks { get; set; }

        public PeopleSearchModel() {
            ReportItems = new List<PeopleSearchReportItem>();
            Departments = new List<SelectListItem>();
            Ranks = new List<SelectListItem>();
        }
    }
}