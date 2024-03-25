using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class Dwarf
    {
        [Key]
        public int DwarfID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<AchievementDwarf> Achivements { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserDwarf> Users { get; set; }

    }
}
