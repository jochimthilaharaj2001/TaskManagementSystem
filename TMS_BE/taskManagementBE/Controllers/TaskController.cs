using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using taskManagementBE.DTOs;
using taskManagementBE.Services.Interfaces;

namespace taskManagementBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskService.GetAllTasks());
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            return Ok(_taskService.GetTaskById(id));
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] CreateTaskItemDto dto)
        {
            return Ok(_taskService.AddTask(dto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskItemDto dto)
        {
            return Ok(_taskService.UpdateTask(id, dto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);

            if (!result.Success) 
            {
                return NotFound(result);
            }

            return Ok(result);


        }

        [HttpPut("{id}/status")]
        public IActionResult ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            var result = _taskService.ChangeStatus(id, dto);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

    }
}
