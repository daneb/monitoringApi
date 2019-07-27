using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUsers
    {
        Task<Users> GetById(int id);
        Task<List<Users>> GetAll();
        Task Create(Users users);
        Task Delete(int id);
        Task Update(Users users);
    }
}