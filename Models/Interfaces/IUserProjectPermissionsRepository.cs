using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUserProjectPermissionsRepository
    {
        Task<List<UserProjectPermissions>> GetById(int id);
        Task Create(UserProjectPermissions projects);
        Task Delete(int id);
        Task Update(UserProjectPermissions projects);
        Task<List<UserProjectPermissions>> GetAll();
    }
}