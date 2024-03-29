using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class User
    {

        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Username { get; set; }


        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserAchievement> Achivements { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserDwarf> Dwarves { get; set; }


    }
}
