using System.IO;
using System.Runtime.CompilerServices;
using Services;

namespace DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }
    }
}