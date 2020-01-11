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
    [Route("topics/")]
    public class TopicController : Controller {

        private readonly ITopicService _service;

        public TopicController(ITopicService service) {
            _service = service;
        }

        [HttpGet]
        private async Task<ActionResult> GetAllTopics() {
            var result = await _service.GetAllTopicsHandler();

            return Ok(result); 
        }

        [HttpGet]
        [Route("{Id}")]
        private async Task<ActionResult> GetTopic( [FromRoute][Required] Guid Id) {
            var result = await _service.GetTopicHandler(Id);

            return Ok(result);
        }

        [HttpPost]
        private async Task<ActionResult> CreateTopic([FromBody][Required] TopicDto topicDto) {
            await _service.CreateTopicHandler(topicDto);

            return Ok();
        }

        [HttpPut]
        [Route("{Id}")]
        private async Task<ActionResult> UpdateTopic(
            [FromRoute][Required] Guid Id,
            [FromBody][Required] TopicDto topicDto) {
            await _service.UpdateTopicHandler(Id, topicDto);

            return Ok();
        }

        [HttpDelete]
        [Route("Id")]
        private async Task<ActionResult> DeleteTopic([FromRoute][Required] Guid Id) {
            await _service.DeleteTopicHandler(Id);

            return Ok();
        }
    }
}