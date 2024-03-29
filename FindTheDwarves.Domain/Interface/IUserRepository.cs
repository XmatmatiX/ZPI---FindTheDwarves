using FindTheDwarves.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Domain.Interface
{
    public interface IUserRepository
    {
        int AddNewUser(User user);

        bool IsUsernameTaken(string username);
        bool IsEmailTaken(string email);
        User Login(string email);
    }
}
