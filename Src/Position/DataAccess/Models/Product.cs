using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Product
    {
        public short Product_id { get; set; }
        public byte[] Parameters { get; set; }
        public string Product_desc { get; set; }
    }
}
