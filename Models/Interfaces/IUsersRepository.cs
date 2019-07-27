using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task Create(User user);
        Task Delete(int id);
        Task Update(User user);
    }
}