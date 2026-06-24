using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskManagementBE.DTOs;
using taskManagementBE.Services.Interfaces;

namespace taskManagementBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser(CreateUserDto dto)
        {
            var result = _userService.AddUser(dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Created("", result);
        }

    }
}
