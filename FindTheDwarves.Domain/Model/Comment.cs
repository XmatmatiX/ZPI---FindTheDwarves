using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Model
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Text { get; set; }

        public int DwarfID { get; set; }
        public virtual Dwarf Dwarf { get; set; }

        public int UserID { get; set; }
        public virtual User Author { get; set; }

    }
}
