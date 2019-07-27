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

        public async Task<List<UserProjectPermissions>> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermissions where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermissions>(sQuery, new { ID = id });
                return result.AsList();
            }
        }

        public async Task Create(UserProjectPermissions projects)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(UserProjectPermissions projects)
        {
            throw new System.NotImplementedException();
        }
    }
}