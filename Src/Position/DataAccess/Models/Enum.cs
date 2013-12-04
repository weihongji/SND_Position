using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public enum WorkPlace
    {
        Any = 0,
        Well = 1,
        Ground = 2
    }

    public enum PeopleCountSearchType
    {
        Region = 1,
        Department = 2,
        WorkType = 3,
        Rank = 4
    }

    public enum MapSize
    {
        Small = 1,
        Medim = 2,
        Large = 3
    }
}
