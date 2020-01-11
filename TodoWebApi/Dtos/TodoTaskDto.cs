using Newtonsoft.Json;
using System;

namespace TodoWebApi.Dtos {
    public class TodoTaskDto {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "deadline")]
        public DateTime Deadline { get; set; }
    }
}