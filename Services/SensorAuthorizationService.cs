using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models;
using Models.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class SensorAuthorizationService : ISensorAuthorizationService
    {
        private readonly IUserProjectPermissionsRepository _permissionsRepository;

        public SensorAuthorizationService(IUserProjectPermissionsRepository userProjectPermissionsRepo)
        {
            _permissionsRepository = userProjectPermissionsRepo;
        }

        public async Task<bool> IsAuthorized(int userId, int sensorId, Permissions requestedPermissions)
        {
            return await _permissionsRepository.CanAccess(userId, sensorId, requestedPermissions);
        }
    }
}