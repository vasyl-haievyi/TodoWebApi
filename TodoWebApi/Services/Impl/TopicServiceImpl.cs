using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApi.Dtos;
using TodoWebApi.Daos;
using MongoDB.Driver;
using TodoWebApi.Properties;
using System.Linq;
using TodoWebApi.Utils;
using MongoDB.Bson;

namespace TodoWebApi.Services.Impl {
    public class TopicServiceImpl : ITopicService {
        private readonly IMongoCollection<TopicDao> _topicsCollection;
        private readonly IMongoCollection<TodoTaskDao> _tasksCollection;
        private readonly DatabaseSettings _databaseSetings;

        public TopicServiceImpl(DatabaseSettings settings) {
            _databaseSetings = settings;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(_databaseSetings.TodoDatabase);

            _topicsCollection = database.GetCollection<TopicDao>(_databaseSetings.TopicCollection);
            _tasksCollection = database.GetCollection<TodoTaskDao>(_databaseSetings.TaskCollection);
        }
        public async Task CreateTopicHandler(TopicDto topicDto) {
            var topicDao = Mapper.MapTopicDtoToDao(topicDto);
            topicDao.Uuid = Guid.NewGuid();

            await _topicsCollection.InsertOneAsync(topicDao);

            foreach(var taskDto in topicDto.Tasks) {
                var taskDao = Mapper.MapTaskDtoToDao(taskDto);
                taskDao.Uuid = Guid.NewGuid();
                taskDao.TopicUuid = topicDao.Uuid;

                await _tasksCollection.InsertOneAsync(taskDao);
            }
        }

        public async Task<bool> DeleteTopicHandler(Guid Uuid) {
            var topic = (await _topicsCollection.FindAsync(topicDao => topicDao.Uuid == Uuid)).FirstOrDefault();
            if(topic == null) {
                return false;
            }

            await _topicsCollection.DeleteOneAsync(topicDao => topicDao.Uuid == Uuid);
            await _tasksCollection.DeleteManyAsync(taskDao => taskDao.TopicUuid == Uuid);
            return true;
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsHandler() {
            var daos = (await _topicsCollection.FindAsync(topic => true)).ToList();

            return daos.Select( 
                dao => Mapper.MapTopicDaoToDto(
                    dao, 
                    _tasksCollection
                        .Find(task => task.TopicUuid == dao.Uuid)
                        .ToList()
                        .Select(dao => Mapper.MapTaskDaoToDto(dao))
                )
            );
        }

        public async Task<TopicDto> GetTopicHandler(Guid Uuid) {
            var topic = (await _topicsCollection.FindAsync(dao => dao.Uuid == Uuid)).FirstOrDefault();

            if(topic == null){
                return null;
            }
            return Mapper.MapTopicDaoToDto(
                topic, 
                _tasksCollection
                    .Find(task => task.TopicUuid == topic.Uuid)
                    .ToList()
                    .Select(dao => Mapper.MapTaskDaoToDto(dao)));
        }

        public async Task<bool> UpdateTopicHandler(Guid Uuid, TopicDto topicDto) {
            var topic = (await _topicsCollection.FindAsync(dao => dao.Uuid == Uuid)).FirstOrDefault();

            if(topic == null) {
                return false;
            }

            _tasksCollection.DeleteMany(task => task.TopicUuid == Uuid);

            var filter = Builders<TopicDao>.Filter.Eq("Uuid", Uuid);
            var update = Builders<TopicDao>.Update.Set("name", topicDto.Name);

            _topicsCollection.UpdateOne(filter,update);

            foreach(var taskDto in topicDto.Tasks) {
                var taskDao = Mapper.MapTaskDtoToDao(taskDto);
                taskDao.TopicUuid = Uuid;
                taskDao.Uuid = Guid.NewGuid();
                
                _tasksCollection.InsertOne( taskDao );
            }

            return true;
        }
    }
}