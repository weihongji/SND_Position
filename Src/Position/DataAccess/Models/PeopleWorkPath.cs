using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PeopleWorkPath
    {
        public int Path_id { get; set; }
        public int Step_id { get; set; }
        public int People_id { get; set; }
        public short Position_id { get; set; }
        public System.DateTime Begin_time { get; set; }
        public System.DateTime End_time { get; set; }
        public byte Check_status { get; set; }
        public virtual Person Person { get; set; }
        public virtual Position Position { get; set; }
    }
}
