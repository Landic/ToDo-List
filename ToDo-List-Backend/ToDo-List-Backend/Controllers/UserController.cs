using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDo_List_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            var result = await userService.RegisterUser(userCreateDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }        
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await userService.LoginUser(userLoginDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
