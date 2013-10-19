using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PeopleShift
    {
        public int PeopleId { get; set; }
        public short ShiftId { get; set; }
        public System.DateTime FirstShiftTime { get; set; }
        public System.DateTime LastShiftTime { get; set; }
        public virtual Person Person { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
