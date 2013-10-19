using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PeopleSender
    {
        public int People_id { get; set; }
        public int Sender_id { get; set; }
        public System.DateTime First_use_time { get; set; }
        public System.DateTime Last_use_time { get; set; }
        public virtual Person Person { get; set; }
        public virtual Sender Sender { get; set; }
    }
}
