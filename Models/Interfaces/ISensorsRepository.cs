using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISensors
    {
        Task<Sensors> GetById(int id);
        Task<List<Sensors>> GetAll();
        Task Create(Sensors sensor);
        Task Delete(int id);
        Task Update(Sensors sensors);

    }
}