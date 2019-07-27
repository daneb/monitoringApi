using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DTO;

namespace Models.Interfaces
{
    public interface ISensorTypesRepository
    {
        Task<SensorType> GetById(int id);
        Task<List<SensorType>> GetAll();
        Task<int> Create(SensorType sensorType);
        Task<bool> Delete(int id);
        Task<bool> Update(SensorType sensorType);
    }
}