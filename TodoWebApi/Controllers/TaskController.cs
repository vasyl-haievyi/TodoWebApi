using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoWebApi.Dtos;
using TodoWebApi.Services;

namespace TodoWebApi.Controllers {

    [ApiController]
    [Route("tasks")]
    public class TaskController : Controller {

        private readonly ITaskService _service;

        public TaskController(ITaskService service) {
            _service = service;
        }

        [HttpGet]
        private async Task<IActionResult> GetAllTasks() {
            var result = await _service.GetAllTasksHandler();
            return Ok(result); 
        }

        [HttpGet]
        [Route("{Uuid}")]
        private async Task<IActionResult> GetTask( [FromRoute][Required] Guid Uuid) {
            var result = await _service.GetTaskHandler(Uuid);

            if(result == null) {
                return NotFound(); 
            }
            return Ok(result);
        }

        [HttpPost]
        private async Task<IActionResult> CreateTopic([FromBody][Required] TodoTaskDto taskDto) {
            await _service.CreateTaskHandler(taskDto);

            return Ok();
        }

        [HttpPut]
        [Route("{Uuid}")]
        private async Task<IActionResult> UpdateTask(
            [FromRoute][Required] Guid Uuid,
            [FromBody][Required] TodoTaskDto taskDto) {
            if ( ! await _service.UpdateTaskHandler(Uuid, taskDto) ) {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{Uuid}")]
        private async Task<IActionResult> DeleteTask([FromRoute][Required] Guid Uuid) {
            if ( ! await _service.DeleteTaskHandler(Uuid) ) {
                return NotFound();
            }

            return Ok();
        }
    }
}