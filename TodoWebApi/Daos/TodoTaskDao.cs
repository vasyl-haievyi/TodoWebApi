using System;

namespace TodoWebApi.Daos {
    public class TodoTaskDao {
        public string Name { get; set; }
        public Guid Uuid { get; set; }
        public Guid TopicUuid { get; set; }

    }
}