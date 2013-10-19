using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class PositionSearchReportItem
	{
		public int Index { get; set; }
		public int PeopleId { get; set; }
		public string PeopleName { get; set; }
		public string Gender { get; set; }
		public string DepartmentName { get; set; }
		public string WorkType { get; set; }
		public string Rank { get; set; }
		public int? SenderId { get; set; }
		public short? PositionId { get; set; }
		public string PositionName { get; set; }
	}
}
