using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISensorsRepository
    {
        Task<Sensor> GetById(int id);
        Task<List<Sensor>> GetAll();
        Task<int> Create(Sensor sensor);
        Task<bool> Delete(int id);
        Task<bool> Update(Sensor sensor);

    }
}