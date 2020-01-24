using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace TodoWebApi.Dtos {
    public class TopicDto {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "uuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Uuid { get; set; }
    }
}