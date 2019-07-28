using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task<int> Create(User user);
        Task<bool> Delete(int id);
        Task<bool> Update(User user);
        Task<User> Authenticate(string email, string password);
    }
}