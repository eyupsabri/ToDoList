using Dapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsers()
        {
            string query = "Select * from Users";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<User>(query);
                return values.ToList();
            }
        }

    }
}
