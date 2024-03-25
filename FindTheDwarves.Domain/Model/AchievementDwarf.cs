using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class AchievementDwarf
    {
        public int AchievementID { get; set; }
        public Achievement Achievement { get; set; }


        public int DwarfID { get; set; }
        public Dwarf Dwarf { get; set; }
    }
}
