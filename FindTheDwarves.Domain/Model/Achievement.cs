using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class Achievement
    {
        public int AchievementID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<AchievementDwarf> Dwarves { get; set; }

        public virtual ICollection<UserAchievement> Users { get; set; }



    }
}
