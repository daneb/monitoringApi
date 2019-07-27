using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace Models.Interfaces
{
    public interface ISensorTypesRepository
    {
        Task<SensorType> GetById(int id);
        Task<List<SensorType>> GetAll();
        Task<int> Create(SensorType sensorType);
        Task Delete(int id);
        Task Update(SensorType sensorType);
    }
}