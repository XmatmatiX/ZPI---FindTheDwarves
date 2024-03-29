using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Users
{
    public class RegisterUserDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(16,MinimumLength =5)]
        public string Password { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 5)]
        public string Username { get; set; }

    }
}
