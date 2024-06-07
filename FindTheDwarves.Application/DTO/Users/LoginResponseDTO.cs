using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Users
{
    public class LoginResponseDTO
    {
        public string JWT { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}
