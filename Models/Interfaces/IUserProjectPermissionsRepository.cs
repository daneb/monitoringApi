using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUserProjectPermissionsRepository
    {
        Task<List<UserProjectPermission>> GetById(int id);
        Task Create(UserProjectPermission projects);
        Task Delete(int id);
        Task Update(UserProjectPermission projects);
        Task<List<UserProjectPermission>> GetAll();
    }
}