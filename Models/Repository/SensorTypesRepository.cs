using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using DTO;
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

        public async Task<SensorType> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from SensorTypes where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<SensorType>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<SensorType>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Name, Description from SensorTypes";
                conn.Open();
                var result = await conn.QueryAsync<SensorType>(sQuery);
                return result.AsList();
            }
        }

        public async Task<int> Create(SensorType sensorType)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync<SensorType>(sensorType);
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.DeleteAsync(new SensorType { Id = id });
                return result;
            }
        }

        public async Task<bool> Update(SensorType sensorType)
        {

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.UpdateAsync<SensorType>(sensorType);
                return result;
            }
        }
    }
}