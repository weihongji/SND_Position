using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Person
    {
        public Person()
        {
            this.PeopleShifts = new List<PeopleShift>();
            this.PeopleWorkPaths = new List<PeopleWorkPath>();
            this.PeopleSenders = new List<PeopleSender>();
        }

        public int People_id { get; set; }
        public string People_name { get; set; }
        public byte Gender { get; set; }
        public byte[] Picture { get; set; }
        public short Dept_id { get; set; }
        public short Worktype_id { get; set; }
        public short Rank_id { get; set; }
        public System.DateTime Birthday { get; set; }
        public System.DateTime Enroll_time { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ID_Number { get; set; }
        public byte Blood_type { get; set; }
        public string Allergy { get; set; }
        public string Linkman_name { get; set; }
        public string Linkman_dept { get; set; }
        public string Linkman_phone { get; set; }
        public string People_info { get; set; }
        public virtual Department Department { get; set; }
        public virtual Rank Rank { get; set; }
        public virtual WorkType WorkType { get; set; }
        public virtual ICollection<PeopleShift> PeopleShifts { get; set; }
        public virtual ICollection<PeopleWorkPath> PeopleWorkPaths { get; set; }
        public virtual ICollection<PeopleSender> PeopleSenders { get; set; }
    }
}
