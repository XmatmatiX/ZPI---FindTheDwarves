using FindTheDwarves.Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.Interface
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserDTO dto);
        LoginResponseDTO GenerateJWT(LoginDTO dto);

        ListShowUserDTO GetUsers();
        void DeleteUser(int userID);
    }
}
