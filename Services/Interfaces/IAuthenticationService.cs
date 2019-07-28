using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Authenticate(string email, string password, string secret);

        string Hash(string password);
    }
}