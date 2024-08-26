using Entities.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository
    {
        public Task<List<UserResponseDto>> GetAllUsers();

        public Task<UserDto> GetByEmail(string email);
    }
}
