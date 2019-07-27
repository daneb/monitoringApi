using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISensorTypesRepository
    {
        Task<SensorTypes> GetById(int id);
        Task<List<SensorTypes>> GetAll();
        Task Create(SensorTypes sensorTypes);
        Task Delete(int id);
        Task Update(SensorTypes sensorTypes);
    }
}