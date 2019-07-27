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
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IConfiguration _config;

        public ProjectsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<Project> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from Projects where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Project>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Project>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from Projects";
                conn.Open();
                var result = await conn.QueryAsync<Project>(sQuery);
                return result.AsList();
            }
        }

        public async Task<int> Create(Project project)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync<Project>(project);
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.DeleteAsync(new Project { Id = id });
                return result;
            }
        }

        public async Task<bool> Update(Project project)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.UpdateAsync<Project>(project);
                return result;
            }
        }
    }
}