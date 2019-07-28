using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Interfaces;
using Services.Helpers;
using Services.Interfaces;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserProjectPermissionsRepository _userProjectPermissionsRepository;

        public const string PassPhrase = "@#$^asdv";

        public AuthenticationService(IUsersRepository usersRepo, IUserProjectPermissionsRepository userPermissions)
        {
            _usersRepository = usersRepo;
            _userProjectPermissionsRepository = userPermissions;
        }

        public async Task<User> Authenticate(string email, string password, string secret)
        {
            var user = await _usersRepository.GetByEmail(email);

            if (user == null || password == "")
                return null;

            var expectedPassword = Hash(password);

            if (expectedPassword != user.PasswordHash)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Administrator" : "Reader"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public string Hash(string password)
        {
            return Encryption.Encrypt(password, PassPhrase);
        }
    }
}