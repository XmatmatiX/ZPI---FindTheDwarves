using FindTheDwarves.Application.DTO.Users;
using FindTheDwarves.Application.Interface;
using FindTheDwarves.Domain.Interface;
using FindTheDwarves.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.Service
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJWT(LoginDTO dto)
        {
            var user = _userRepository.Login(dto.Email);

            if (user == null)
            {
                throw new Exception("Bad email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Bad email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expire,
                signingCredentials: credential
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);

        }

        public int RegisterUser(RegisterUserDTO dto)
        {
            if (_userRepository.IsEmailTaken(dto.Email))
            {
                return -1;
            }

            if (_userRepository.IsUsernameTaken(dto.Username))
            {
                return -2;
            }

            User user = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                RoleID = 1
            };

            var hashedPassword = _passwordHasher.HashPassword(user,dto.Password);

            user.Password = hashedPassword;

            return _userRepository.AddNewUser(user);

        }
    }
}
