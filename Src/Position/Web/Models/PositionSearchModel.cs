using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Models
{
	public class PositionSearchModel
	{
		public List<PositionSearchReportItem> ReportItems { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Receivers { get; set; }
        public List<SelectListItem> Positions { get; set; }

		public PositionSearchModel() {
			ReportItems = new List<PositionSearchReportItem>();
            Regions = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
            Receivers = new List<SelectListItem>();
            Positions = new List<SelectListItem>();
		}
	}
}