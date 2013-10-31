using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class CurrentPositionReport
    {
        public int Sender_id { get; set; }
        public byte Branch_id { get; set; }
        public byte Receiver_id { get; set; }
        public short Position_id { get; set; }
        public short Distance { get; set; }
        public System.DateTime Report_time { get; set; }
    }
}
