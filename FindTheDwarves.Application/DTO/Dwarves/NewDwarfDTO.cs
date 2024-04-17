using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Dwarves
{
    public class NewDwarfDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ActivationCode { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
