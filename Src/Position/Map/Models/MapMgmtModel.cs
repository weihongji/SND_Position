using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map.Models
{
    public class MapMgmtModel
    {
        public List<FengYa> FengYaList { get; set; }

        public MapMgmtModel() {
            FengYaList = new List<FengYa>();
        }
    }
}