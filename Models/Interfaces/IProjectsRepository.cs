using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IProjectsRepository
    {
        Task<Project> GetById(int id);
        Task<List<Project>> GetAll();
        Task<int> Create(Project project);
        Task<bool> Delete(int id);
        Task<bool> Update(Project project);
    }
}