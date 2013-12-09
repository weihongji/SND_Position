using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class PeopleCountModel
    {
        public PeopleCountSearchType SearchType { get; set; }
        public List<PeopleCountReportItem> ReportItems { get; set; }

        public PeopleCountModel() {
            SearchType = PeopleCountSearchType.Region;
            ReportItems = new List<PeopleCountReportItem>();
        }
    }
}