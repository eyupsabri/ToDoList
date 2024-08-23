using Business;
using Entities.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Helpers;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContoller : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _config;

        public UserContoller(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();


            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = await _userService.AuthenticateUser(login);
            if (user != null)
            {
                var jwtIssuer = _config.GetSection("JWT_ISSUER").Get<string>();
                var jwtKey = _config.GetSection("JWT_KEY").Get<string>();

                var accessToken = TokenHelper.GenerateToken(user.Email, false, jwtKey, jwtIssuer);
                var refreshToken = TokenHelper.GenerateToken(user.Email, true, jwtKey, jwtIssuer);
                response = Ok(new { accessToken = accessToken, refreshToken = refreshToken });
            }

            return response;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            var jwtIssuer = _config.GetSection("JWT_ISSUER").Get<string>();
            var jwtKey = _config.GetSection("JWT_KEY").Get<string>();
            var response = TokenHelper.ValidateToken(token, jwtKey, jwtIssuer);
            if (response.IsValidToken && !response.IsExpired && response.TokenType == "refresh")
            {
                var accessToken = TokenHelper.GenerateToken(response.Email, false, jwtKey, jwtIssuer);
                var refreshToken = TokenHelper.GenerateToken(response.Email, true, jwtKey, jwtIssuer);
                return Ok(new { accessToken = accessToken, refreshToken = refreshToken });
            }
            return Unauthorized();
        }
    }
}
