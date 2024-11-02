using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Models;
using CookBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IJwtTokensRepository _jwtTokensRepository;
        public UserController(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
    IJwtTokensRepository jwtTokensRepository)
        {
            _jwtTokensRepository = jwtTokensRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> CreateUser(UserDto userDto)
        {
             var user = await _userRepository.CreateUserAsync(userDto);
            var token = GenerateAndStoreToken(user);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto userDto)
        {
            var user = await _userRepository.LoginAsync(userDto);
            var token = GenerateAndStoreToken(user); 
            return Ok(token);
        }

        [HttpDelete("logout")]
        public IActionResult Logout(int userId)
        {
            _jwtTokensRepository.Remove(userId);
            return Ok();
        }


        private string GenerateAndStoreToken(User user)
        {
            var token = _jwtTokenGenerator.GenerateToken(user);
            _jwtTokensRepository.Update(user.Id, token);

            return token;
        }
    }
}
