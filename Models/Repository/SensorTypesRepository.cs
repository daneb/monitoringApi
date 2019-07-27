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
    public class SensorTypesRepository : ISensorTypesRepository
    {
        private readonly IConfiguration _config;

        public SensorTypesRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<SensorTypes> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from SensorTypes where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<SensorTypes>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<SensorTypes>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task Create(SensorTypes sensorTypes)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(SensorTypes sensorTypes)
        {
            throw new System.NotImplementedException();
        }
    }
}