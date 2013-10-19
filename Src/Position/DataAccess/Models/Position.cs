using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Position
    {
        public Position()
        {
            this.Branches = new List<Branch>();
            this.PeopleWorkPaths = new List<PeopleWorkPath>();
            this.Receivers = new List<Receiver>();
        }

        public short Position_id { get; set; }
        public int Position_x { get; set; }
        public int Position_y { get; set; }
        public int Position_z { get; set; }
        public double Position_sin { get; set; }
        public double Position_cos { get; set; }
        public double Position_vcos { get; set; }
        public string Position_desc { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<PeopleWorkPath> PeopleWorkPaths { get; set; }
        public virtual ICollection<Receiver> Receivers { get; set; }
    }
}
