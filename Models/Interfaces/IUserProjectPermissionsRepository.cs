using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUserProjectPermissions
    {
        Task<UserProjectPermissions> GetById(int id);
        Task<List<UserProjectPermissions>> GetAll();
        Task Create(UserProjectPermissions projects);
        Task Delete(int id);
        Task Update(UserProjectPermissions projects);
    }
}