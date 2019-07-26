using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISensorsRepository
    {
        Task<Sensors> GetById(int id);
        Task<List<Sensors>> GetByName(string Name);
    }
}