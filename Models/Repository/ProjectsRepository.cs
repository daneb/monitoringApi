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

        public async Task Create(Project project)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(Project project)
        {
            throw new System.NotImplementedException();
        }
    }
}