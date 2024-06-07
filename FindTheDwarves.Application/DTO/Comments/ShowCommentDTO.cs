using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Comments
{
    public class ShowCommentDTO
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
    }
}
