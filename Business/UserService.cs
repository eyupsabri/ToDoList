using DAL;
using Entities.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDto> AuthenticateUser(UserLoginDto user)
        {
            var checkUser = await _userRepo.GetByEmail(user.Email);
            if (checkUser != null && checkUser.PasswordHash == user.Password)
            {
                return checkUser;
            }
            return null;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            return users;
        }


    }
}
