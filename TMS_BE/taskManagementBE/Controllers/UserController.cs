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

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetUserById(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserDto dto)
        {
            var result = _userService.AddUser(dto);

            if (!result.Success)
                return BadRequest(result);

            return Created("", result);
        }

        [HttpGet("{id}/tasks")]
        public IActionResult GetUserWithTasks(int id)
        {
            var result = _userService.GetUserWithTasks(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

    }
}
