using System.Xml.Linq;
using Application.DTOs.Task;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDo_List_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await taskService.GetAllTasks();
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] string name)
        {
            var result = await taskService.SearchTask(name);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var result = await taskService.CreatedTask(taskCreateDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto taskUpdateDto)
        {
            var result = await taskService.UpdateTask(id, taskUpdateDto);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await taskService.DeleteTask(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
