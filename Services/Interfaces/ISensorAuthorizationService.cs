using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface ISensorAuthorizationService
    {
        Task<bool> IsAuthorizedBySensorId(int userId, int sensorId, Permissions requestedPermissions);
        Task<bool> IsAuthorizedByProjectId(int userId, int projectId, Permissions requestedPermissions);
        Task<bool> IsAuthorizedBySensorIdAndProjectId(int userId, int sensorId, int projectId, Permissions requestedPermissions);

        
    }
}