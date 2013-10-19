using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            this.Branches = new List<Branch>();
            this.Receivers = new List<Receiver>();
            this.Senders = new List<Sender>();
        }

        public short Product_id { get; set; }
        public byte[] Parameters { get; set; }
        public string Product_desc { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Receiver> Receivers { get; set; }
        public virtual ICollection<Sender> Senders { get; set; }
    }
}
