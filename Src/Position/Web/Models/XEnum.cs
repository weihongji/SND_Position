using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess.Models;
using System.Web.Mvc;

namespace Web.Models
{
    public enum PositionSearchType
    {
        Region = 1,
        Branch = 2,
        Receiver = 3,
        Position = 4
    }
}