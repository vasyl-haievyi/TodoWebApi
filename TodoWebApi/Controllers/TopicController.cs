using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoWebApi.Dtos;

namespace TodoWebApi.Controllers {
    [ApiController]
    [Route("topics/")]
    public class TopicController : Controller {

        [HttpGet]
        private Task<ActionResult> GetAllTopics() {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{Id}")]
        private Task<ActionResult> GetTopic( [FromRoute][Required] Guid Id) {
            throw new NotImplementedException();
        }

        [HttpPost]
        private Task<ActionResult> CreateTopic([FromBody][Required] TopicDto topicDto) {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{Id}")]
        private Task<ActionResult> UpdateTopic(
            [FromRoute][Required] Guid Id,
            [FromBody][Required] TopicDto topicDto) {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Id")]
        private Task<ActionResult> DeleteTopic([FromRoute][Required] Guid Id) {
            throw new NotImplementedException();
        }
    }
}