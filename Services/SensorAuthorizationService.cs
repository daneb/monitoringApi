using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LazyCache;
using Models;
using Models.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class SensorAuthorizationService : ISensorAuthorizationService
    {
        private readonly IUserProjectPermissionsRepository _permissionsRepository;
        private readonly IAppCache _cache;

        public SensorAuthorizationService(IUserProjectPermissionsRepository userProjectPermissionsRepo, IAppCache cache)
        {
            _permissionsRepository = userProjectPermissionsRepo;
            _cache = cache;
        }

        public async Task<bool> IsAuthorized(int userId, int sensorId, Permissions requestedPermissions)
        {
            string permission = requestedPermissions.ToString();

            List<SensorUserProjectPermissions> permissions =
                await _cache.GetOrAddAsync("SensorPermissions",() => _permissionsRepository.GetPermissionsWithSensorAndProjectId());

            bool authorized = permissions.Any(x => x.SensorId == sensorId && x.UserId == userId && x.Permission == permission);

            return authorized;
        }
    }
}