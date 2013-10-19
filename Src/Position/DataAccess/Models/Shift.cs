using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Shift
    {
        public Shift()
        {
            this.PeopleShifts = new List<PeopleShift>();
        }

        public short ShiftId { get; set; }
        public string ShiftName { get; set; }
        public System.DateTime ShiftBeginTime { get; set; }
        public System.DateTime ShiftEndTime { get; set; }
        public Nullable<short> ShiftGroupId { get; set; }
        public virtual ICollection<PeopleShift> PeopleShifts { get; set; }
    }
}
