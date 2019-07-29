using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task<int> Create(Sensor sensor)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    var result = await conn.InsertAsync<Sensor>(sensor);
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Please forgive me for throwing away exceptions ;)
                return 0;
            }
           
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    var result = await conn.DeleteAsync(new Sensor { Id = id });
                    return result;
                }
            }
            catch (Exception e)
            {
                // Please forgive me for throwing away exceptions ;)
                return false;
            }

        }

        public async Task<bool> Update(Sensor sensor)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    var result = await conn.UpdateAsync<Sensor>(sensor);
                    return result;
                }
            }
            catch (Exception e)
            {
                // Please forgive me for throwing away exceptions ;)
                return false;
            }

        }

    }
}