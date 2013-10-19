using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Receiver
    {
        public byte Branch_id { get; set; }
        public byte Receiver_id { get; set; }
        public short Status { get; set; }
        public short Product_id { get; set; }
        public short Position_id { get; set; }
        public byte[] Parameters { get; set; }
        public string Receiver_info { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Position Position { get; set; }
        public virtual Product Product { get; set; }
    }
}
