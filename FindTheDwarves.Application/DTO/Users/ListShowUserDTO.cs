using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Users
{
    public class ListShowUserDTO
    {

        public List<ShowUserDTO> UserList { get; set; }
        public int Count { get; set; }

        public ListShowUserDTO()
        {
            UserList= new List<ShowUserDTO>();
        }

    }
}
