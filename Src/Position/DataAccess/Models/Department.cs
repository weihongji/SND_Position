using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            this.People = new List<Person>();
        }

        public short Dept_id { get; set; }
        public string Dept_name { get; set; }
        public string Dept_fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Dept_info { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
