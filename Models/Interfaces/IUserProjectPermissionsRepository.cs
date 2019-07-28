using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUserProjectPermissionsRepository
    {
        Task<UserProjectPermission> GetById(int id);
        Task<List<UserProjectPermission>> GetByUserId(int userId);
        Task<List<UserProjectPermission>> GetAll();
        Task<int> Create(UserProjectPermission projects);
        Task<bool> Delete(int id);
        Task<bool> Update(UserProjectPermission projects);

    }
}