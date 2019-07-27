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
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration _config;

        public UsersRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("Monitoring"));

        public async Task<User> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Email, PasswordHash, Name, Surname, IsAdmin from Users where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<User>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Email, PasswordHash, Name, Surname, IsAdmin from Users";
                conn.Open();
                var result = await conn.QueryAsync<User>(sQuery);
                return result.AsList();
            }
        }

        public async Task Create(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}