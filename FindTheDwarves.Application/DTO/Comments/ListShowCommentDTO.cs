using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Comments
{
    public class ListShowCommentDTO
    {
        public List<ShowCommentDTO> CommentList { get; set; }
        public int Count { get; set; }

        public ListShowCommentDTO() 
        {
            CommentList = new List<ShowCommentDTO>();
        }

    }
}
