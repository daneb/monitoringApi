using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IProjectsRepository
    {
        Task<Projects> GetById(int id);
        Task<List<Projects>> GetAll();
        Task Create(Projects projects);
        Task Delete(int id);
        Task Update(Projects projects);
    }
}