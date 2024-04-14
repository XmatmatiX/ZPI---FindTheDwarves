using FindTheDwarves.Domain.Interface;
using FindTheDwarves.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Infrastructure.Repository
{
    public class DwarfRepository : IDwarfRepository
    {
        private readonly Context _context;

        public DwarfRepository(Context context)
        {
            _context = context;
        }

        public int AddDwarf(Dwarf dwarf)
        {
            _context.Dwarves.Add(dwarf);
            _context.SaveChanges();

            return dwarf.DwarfID;
        }

        public void DeleteDwarf(Dwarf dwarf)
        {
            _context.Dwarves.Remove(dwarf);
            _context.SaveChanges();
        }

        public Dwarf GetDwarfByName(string name)
        {
            var dwarf = _context.Dwarves.Where(d => d.Name == name).FirstOrDefault();

            return dwarf;
        }

        public Dwarf GetDwarfByCode(string code)
        {
            var dwarf = _context.Dwarves.Where(d=>d.ActivationCode== code).FirstOrDefault();

            return dwarf;
        }

        public List<Dwarf> GetDwarves()
        {
            var dwarves = _context.Dwarves.ToList();

            return dwarves;
        }

        public int UpdateDwarf(Dwarf dwarf)
        {
            var check = _context.Dwarves.Where(d => d.DwarfID == dwarf.DwarfID).FirstOrDefault();
            if (check == null) 
            {
                return -1;
            }

            _context.Attach(dwarf);
            _context.Entry(dwarf).Property("Name").IsModified= true;
            _context.Entry(dwarf).Property("ActivationCode").IsModified = true;
            _context.Entry(dwarf).Property("Description").IsModified = true;
            _context.SaveChanges();
            return dwarf.DwarfID;
        }

        public List<Comment> GetDwarfComments(int dwarfID)
        {
            var comments = _context.Comments.Where(c => c.DwarfID == dwarfID).ToList();

            if (comments.Count == 0)
            {
                return null;
            }

            return comments;
        }

        public Dwarf GetDwarfByID(int ID)
        {
            var dwarf = _context.Dwarves.FirstOrDefault(d=>d.DwarfID == ID);

            return dwarf;
        }

        public bool CanBeCommented(int userID, int dwarfID)
        {
            var result = _context.Users.Include(u=>u.Dwarves).FirstOrDefault(u => u.UserID == userID);
            foreach (var dwarf in result.Dwarves)
            {
                if (dwarf.DwarfID == dwarfID)
                {
                    return true;
                }
            }
            return false;
        }

        public int AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment.CommentID;
        }

        public void ClaimDwarf(UserDwarf userDwarf)
        {

            _context.UserDwarves.Add(userDwarf);
            _context.SaveChanges();
        }

        public bool CheckClaim(UserDwarf userDwarf)
        {
            var result = _context.UserDwarves.Where(ud => ud.UserID == userDwarf.UserID && ud.DwarfID == userDwarf.DwarfID).Any();
            
            return result;
        }

        public List<Dwarf> GetUserDwarves(int userID)
        {
            var userDwarves = _context.Users.Include(u => u.Dwarves).Where(u => u.UserID == userID).FirstOrDefault().Dwarves;

            List<Dwarf> dwarfList = new List<Dwarf>();

            foreach (var userDwarf in userDwarves) 
            {
                var dwarf = GetDwarfByID(userDwarf.DwarfID);

                dwarfList.Add(dwarf);
            }

            return dwarfList;
        }
    }
}
