using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface ISensorAuthorizationService
    {
        Task<bool> IsAuthorized(int userId, int sensorId, Permissions requestedPermissions);
    }
}