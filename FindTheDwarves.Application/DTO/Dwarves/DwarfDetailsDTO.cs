using FindTheDwarves.Application.DTO.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Dwarves
{
    public class DwarfDetailsDTO
    {

        public int DwarfID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ListShowCommentDTO comments { get; set; }

        public DwarfDetailsDTO()
        {
            comments = new ListShowCommentDTO();
        }


    }
}
