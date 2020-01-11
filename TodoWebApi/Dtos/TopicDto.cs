using Newtonsoft.Json;
using System.Collections.Generic;

namespace TodoWebApi.Dtos {
    public class TopicDto {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tasks")]
        public IEnumerable<TodoTaskDto> Tasks { get; set; }
    }
}