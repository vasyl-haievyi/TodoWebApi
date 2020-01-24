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
    [Route("/tasks")]
    public class TaskController : Controller {

        private readonly ITaskService _service;

        public TaskController(ITaskService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks() {
            var result = await _service.GetAllTasksHandler();
            return Ok(result); 
        }

        [HttpGet]
        [Route("{Uuid}")]
        public async Task<IActionResult> GetTask( [FromRoute][Required] Guid Uuid) {
            var result = await _service.GetTaskHandler(Uuid);

            if(result == null) {
                return NotFound(); 
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody][Required] TodoTaskDto taskDto) {
            var result = await _service.CreateTaskHandler(taskDto);

            return Ok(result);
        }

        [HttpPut]
        [Route("{Uuid}")]
        public async Task<IActionResult> UpdateTask(
            [FromRoute][Required] Guid Uuid,
            [FromBody][Required] TodoTaskDto taskDto) {
            var result =  await _service.UpdateTaskHandler(Uuid, taskDto);
            if ( result == null) {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{Uuid}")]
        public async Task<IActionResult> DeleteTask([FromRoute][Required] Guid Uuid) {
            var result = await _service.DeleteTaskHandler(Uuid);
            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }
    }
}