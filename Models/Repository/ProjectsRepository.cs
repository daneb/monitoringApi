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
    public class ProjectsRepository : IProjects
    {
        private readonly IConfiguration _config;

        public ProjectsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<Projects> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from Projects where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Projects>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Projects>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task Create(Projects projects)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(Projects projects)
        {
            throw new System.NotImplementedException();
        }
    }
}