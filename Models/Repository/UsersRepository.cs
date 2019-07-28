using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Models.Interfaces;
using Services.Interfaces;

namespace Models.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration _config;
        private readonly IUserPasswordHashProvider _userPasswordHashProvider;

        public UsersRepository(IConfiguration config, IUserPasswordHashProvider iUserPasswordHashProvider)
        {
            _config = config;
            _userPasswordHashProvider = iUserPasswordHashProvider;
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

        public async Task<User> GetByEmail(string email)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, Email, PasswordHash, Name, Surname, IsAdmin from Users where Email = @email";
                conn.Open();
                var result = await conn.QueryAsync<User>(sQuery, new { Email = email });
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

        public async Task<int> Create(User user)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync<User>(user);
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.DeleteAsync(new User { Id = id });
                return result;
            }
        }

        public async Task<bool> Update(User user)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.UpdateAsync<User>(user);
                return result;
            }
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await GetByEmail(email);

            if (user == null || password == "")
                return null;

            var expectedPassword = _userPasswordHashProvider.Hash(password);

            if (expectedPassword != user.PasswordHash)
                return null;

            return user;
        }
    }
}