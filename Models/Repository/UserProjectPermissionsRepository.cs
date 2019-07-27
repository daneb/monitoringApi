using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models.Interfaces;

namespace Models.Repository
{
    public class UserProjectPermissionsRepository : IUserProjectPermissionsRepository
    {
        private readonly IConfiguration _config;

        public UserProjectPermissionsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<List<UserProjectPermission>> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermission where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermission>(sQuery, new { ID = id });
                return result.AsList();
            }
        }

        public async Task<List<UserProjectPermission>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermission";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermission>(sQuery);
                return result.AsList();
            }
        }

        public async Task Create(UserProjectPermission projects)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(UserProjectPermission projects)
        {
            throw new System.NotImplementedException();
        }
    }
}