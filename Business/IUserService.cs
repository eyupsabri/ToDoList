using Entities.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsers();
        Task<UserDto> AuthenticateUser(UserLoginDto user);
    }
}
