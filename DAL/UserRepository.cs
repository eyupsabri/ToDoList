using Dapper;
using Entities;
using Entities.DTOs.Users;
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
        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            string query = "Select UserId, UserName, Email, FullName, CreatedAt from Users";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<UserResponseDto>(query);
                return values.ToList();
            }
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            string query = "Select * from Users Where Email = @email";
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<UserDto>(query, parameters);
                return user;
            }
        }
    }
}
