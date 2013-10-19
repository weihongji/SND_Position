using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Lamp
    {
        public string Lamp_id { get; set; }
        public int Sender_id { get; set; }
        public string Lamp_info { get; set; }
        public virtual Sender Sender { get; set; }
    }
}
