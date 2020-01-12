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
    [Route("topics")]
    public class TopicController : Controller {

        private readonly ITopicService _service;

        public TopicController(ITopicService service) {
            _service = service;
        }

        [HttpGet]
        private async Task<IActionResult> GetAllTopics() {
            var result = await _service.GetAllTopicsHandler();
            return Ok(result); 
        }

        [HttpGet]
        [Route("{Uuid}")]
        private async Task<IActionResult> GetTopic( [FromRoute][Required] Guid Uuid) {
            var result = await _service.GetTopicHandler(Uuid);

            if(result == null) {
                return NotFound(); 
            }
            return Ok(result);
        }

        [HttpPost]
        private async Task<IActionResult> CreateTopic([FromBody][Required] TopicDto topicDto) {
            await _service.CreateTopicHandler(topicDto);

            return Ok();
        }

        [HttpPut]
        [Route("{Uuid}")]
        private async Task<ActionResult> UpdateTopic(
            [FromRoute][Required] Guid Uuid,
            [FromBody][Required] TopicDto topicDto) {
            if ( ! await _service.UpdateTopicHandler(Uuid, topicDto) ) {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{Uuid}")]
        private async Task<ActionResult> DeleteTopic([FromRoute][Required] Guid Uuid) {
            if ( ! await _service.DeleteTopicHandler(Uuid) ) {
                return NotFound();
            }

            return Ok();
        }
    }
}