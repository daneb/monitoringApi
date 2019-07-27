using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models.Interfaces;

namespace Models.Repository
{
    public class SensorsRepository : ISensorsRepository
    {
        private readonly IConfiguration _config;

        public SensorsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<Sensor> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, ProjectId, SensorTypeId, Name, Description from Sensors where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Sensor>(sQuery, new {ID = id});
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Sensor>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, ProjectId, SensorTypeId, Name, Description from Sensors";
                conn.Open();
                var result = await conn.QueryAsync<Sensor>(sQuery);
                return result.AsList();
            }
        }

        public async Task Create(Sensor sensor)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(Sensor sensor)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Sensor>> GetByName(string Name)
        {
            throw new System.NotImplementedException();
        }
    }
}