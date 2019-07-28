using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public async Task<UserProjectPermission> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermissions where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermission>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<UserProjectPermission>> GetByUserId(int userId)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermissions where UserId = @UserId";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermission>(sQuery, new { UserId = userId });
                return result.AsList();
            }

        }

        public async Task<List<UserProjectPermission>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, UserId, ProjectId, PermissionContext, Permission from UserProjectPermissions";
                conn.Open();
                var result = await conn.QueryAsync<UserProjectPermission>(sQuery);
                return result.AsList();
            }
        }

        public async Task<int> Create(UserProjectPermission userProjectPermission)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync<UserProjectPermission>(userProjectPermission);
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.DeleteAsync(new UserProjectPermission { Id = id });
                return result;
            }
        }

        public async Task<bool> Update(UserProjectPermission userProjectPermission)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.UpdateAsync<UserProjectPermission>(userProjectPermission);
                return result;
            }
        }
    }
}