using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
