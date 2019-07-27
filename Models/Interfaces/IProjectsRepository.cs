using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IProjectsRepository
    {
        Task<Project> GetById(int id);
        Task<List<Project>> GetAll();
        Task Create(Project project);
        Task Delete(int id);
        Task Update(Project project);
    }
}