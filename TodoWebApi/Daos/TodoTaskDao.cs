using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TodoWebApi.Daos {
    public class TodoTaskDao {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid? Uuid { get; set; }
        public Guid? TopicUuid { get; set; }
        public DateTime Deadline { get; set; }
    }
}