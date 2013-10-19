using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class PeopleOverviewReportItem
	{
		public int PeopleId { get; set; }
		public string PeopleName { get; set; }
		public string DepartmentName { get; set; }
		public string WorkType { get; set; }
		public string Rank { get; set; }
		public string LampId { get; set; }
		public int? SenderId { get; set; }
		public string SenderStatus { get; set; }
	}
}
