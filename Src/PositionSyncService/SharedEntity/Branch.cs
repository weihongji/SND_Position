using System;
using System.Collections.Generic;

namespace SharedEntity
{
    public partial class Branch
    {
        public byte Branch_id { get; set; }
        public short Status { get; set; }
        public short Product_id { get; set; }
        public short Position_id { get; set; }
        public byte Comm_mode { get; set; }
        public byte Can_no { get; set; }
        public byte Uart_port { get; set; }
        public int Ip { get; set; }
        public short Ip_port { get; set; }
        public byte[] Parameters { get; set; }
        public string Branch_info { get; set; }
    }
}
