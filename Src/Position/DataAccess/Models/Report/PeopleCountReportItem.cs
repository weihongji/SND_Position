using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PeopleCountReportItem
    {
        public int Index { get; set; }
        public short Id { get; set; }
        public string Name { get; set; }
        public int Registration { get; set; }
        public int Ground { get; set; }
        public int Well { get; set; }
        public int Number { get; set; } /*For by-region only*/
    }
}
