using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class UserDwarf
    {
        public int UserID { get; set; }
        public User User { get; set; }


        public int DwarfID { get; set; }
        public Dwarf Dwarf { get; set; }
    }
}
