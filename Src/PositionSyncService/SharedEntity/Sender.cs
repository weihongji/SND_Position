using System;
using System.Collections.Generic;

namespace SharedEntity
{
    public partial class Sender
    {
        public int Sender_id { get; set; }
        public byte Sender_type { get; set; }
        public short Status { get; set; }
        public short Product_id { get; set; }
        public byte[] Parameters { get; set; }
        public System.DateTime First_use_time { get; set; }
        public System.DateTime Last_use_time { get; set; }
        public string Sender_info { get; set; }
    }
}
