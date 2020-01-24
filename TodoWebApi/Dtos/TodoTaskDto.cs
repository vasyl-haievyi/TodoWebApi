using System;
using Newtonsoft.Json;

namespace TodoWebApi.Dtos {
    public class TodoTaskDto {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty(PropertyName = "topic_uuid", NullValueHandling = NullValueHandling.Include)]
        public Guid? TopicUuid { get; set; }

        [JsonProperty(PropertyName = "uuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Uuid { get; set; }
    }
}