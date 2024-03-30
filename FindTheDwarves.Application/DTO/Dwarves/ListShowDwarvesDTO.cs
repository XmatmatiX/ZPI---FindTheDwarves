using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.DTO.Dwarves
{
    public class ListShowDwarvesDTO
    {
        public List<ShowDwarfDTO> DwarfList { get; set; }
        public int Count { get; set; }

        public ListShowDwarvesDTO()
        {
            DwarfList = new List<ShowDwarfDTO>();
        }
    }
}
