using FindTheDwarves.Application.DTO.Comments;
using FindTheDwarves.Application.DTO.Dwarves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.Interface
{
    public interface IDwarfService
    {
        int AddNewDwarf(NewDwarfDTO dto);
        int AddComment(NewCommentDTO dto, int userID);

        int ClaimDwarf(ClaimDwarfDTO dto, int userID);

        ListShowDwarvesDTO GetDwarves();
        DwarfDetailsDTO GetDwarfByName(string name);

        
    }
}
