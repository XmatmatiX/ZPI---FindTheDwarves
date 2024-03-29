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
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public int AddNewUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserID;
        }

        public User Login(string email)
        {
            var user = _context.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == email);

            return user;
        }

        public bool IsEmailTaken(string email)
        {
            var users = _context.Users.Where(u => u.Email == email);

            return users.Any();
        }

        public bool IsUsernameTaken(string username)
        {
            var users = _context.Users.Where(u => u.Username == username).ToList();

            return users.Any();
        }
    }
}
