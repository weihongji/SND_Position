using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map.Models
{
    public class FengYa
    {
        public const int ImageHeight = 60;

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Power { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Left {
            get {
                return this.X;
            }
        }
        public int Top {
            get {
                return this.Y - ImageHeight;
            }
        }

        public override string ToString() {
            return string.Format("{0}, Id: {1}, Power: {2}", Name, Id, Power);
        }
    }
}