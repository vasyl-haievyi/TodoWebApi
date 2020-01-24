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
    [Route("/topics")]
    public class TopicController : Controller {

        private readonly ITopicService _service;

        public TopicController(ITopicService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetAllTopics() {
            var result = await _service.GetAllTopicsHandler();
            return Ok(result); 
        }

        [HttpGet]
        [Route("{Uuid}")]
        public async Task<ActionResult<TopicDto>> GetTopic( [FromRoute][Required] Guid Uuid) {
            var result = await _service.GetTopicHandler(Uuid);

            if(result == null) {
                return NotFound(); 
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TopicDto>> CreateTopic([FromBody][Required] TopicDto topicDto) {
            await _service.CreateTopicHandler(topicDto);

            return Ok();
        }

        [HttpPut]
        [Route("{Uuid}")]
        public async Task<ActionResult<TopicDto>> UpdateTopic(
            [FromRoute][Required] Guid Uuid,
            [FromBody][Required] TopicDto topicDto) {
            var res = await _service.UpdateTopicHandler(Uuid, topicDto);
            if ( res == null ) {
                return NotFound();
            }

            return Ok(res);
        }

        [HttpDelete]
        [Route("{Uuid}")]
        public async Task<ActionResult<TopicDto>> DeleteTopic([FromRoute][Required] Guid Uuid) {

            var res = await _service.DeleteTopicHandler(Uuid);
            if ( res == null ) {
                return NotFound();
            }

            return Ok(res);
        }
    }
}