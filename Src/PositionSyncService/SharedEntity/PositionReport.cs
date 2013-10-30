using System;
using System.Collections.Generic;

namespace SharedEntity
{
    public partial class PositionReport
    {
        public int Sender_id { get; set; }
        public byte Branch_id { get; set; }
        public byte Receiver_id { get; set; }
        public short Position_id { get; set; }
        public short Distance { get; set; }
        public System.DateTime Report_time { get; set; }
    }
}
