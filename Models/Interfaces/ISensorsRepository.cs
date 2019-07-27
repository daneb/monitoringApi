using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISensorsRepository
    {
        Task<Sensor> GetById(int id);
        Task<List<Sensor>> GetAll();
        Task Create(Sensor sensor);
        Task Delete(int id);
        Task Update(Sensor sensor);

    }
}