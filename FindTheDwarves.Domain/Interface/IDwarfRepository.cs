using FindTheDwarves.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Interface
{
    public interface IDwarfRepository
    {
        int AddDwarf(Dwarf dwarf);
        int AddComment(Comment comment);


        List<Dwarf> GetDwarves();
        Dwarf GetDwarfByID(int ID);
        Dwarf GetDwarfByName(string name);
        Dwarf GetDwarfByCode(string code);

        List<Comment> GetDwarfComments(int dwarfID);


        bool CheckClaim(UserDwarf userDwarf);

        void ClaimDwarf(UserDwarf userDwarf);

        int UpdateDwarf(Dwarf dwarf);


        void DeleteDwarf(Dwarf dwarf);

        bool CanBeCommented(int userID, int dwarfID);

    }
}
