using Dapper.Contrib.Extensions;

namespace Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        [Write(false)]
        [Computed]
        public string Password { get; set; }

        [Write(false)]
        [Computed]
        public string Token { get; set; }

        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }

    }
}