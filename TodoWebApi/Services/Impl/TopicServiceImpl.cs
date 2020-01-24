using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoWebApi.Daos;
using TodoWebApi.Dtos;
using TodoWebApi.Properties;
using TodoWebApi.Utils;
using Microsoft.Extensions.Options;

namespace TodoWebApi.Services.Impl {
    public class TopicServiceImpl : ITopicService {
        private readonly IMongoCollection<TopicDao> _topicsCollection;
        private readonly DatabaseSettings _databaseSetings;

        public TopicServiceImpl(IOptions<DatabaseSettings> opSettings) {
            _databaseSetings = opSettings.Value;

            var client = new MongoClient(_databaseSetings.ConnectionString);
            var database = client.GetDatabase(_databaseSetings.TodoDatabase);

            _topicsCollection = database.GetCollection<TopicDao>(_databaseSetings.TopicCollection);
        }
        public async Task<TopicDto> CreateTopicHandler(TopicDto topicDto) {
            var topicDao = Mapper.MapTopicDtoToDao(topicDto);
            topicDao.Uuid = Guid.NewGuid();
            try{
                await _topicsCollection.InsertOneAsync(topicDao);
            }catch {
                return null;
            }

            topicDto.Uuid = topicDao.Uuid;
            return topicDto;
        }

        public async Task<TopicDto> DeleteTopicHandler(Guid Uuid) {
            var topic = (await _topicsCollection.FindAsync(topicDao => topicDao.Uuid == Uuid)).FirstOrDefault();
            if (topic == null) {
                return null;
            }

            try {
                await _topicsCollection.DeleteOneAsync(topicDao => topicDao.Uuid == Uuid);
            }catch {
                return null;
            }
            return Mapper.MapTopicDaoToDto(topic);
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsHandler() {
            var daos = (await _topicsCollection.FindAsync(topic => true)).ToList();

            return daos.Select(
                dao => Mapper.MapTopicDaoToDto(dao)
            );
        }

        public async Task<TopicDto> GetTopicHandler(Guid Uuid) {
            var topic = (await _topicsCollection.FindAsync(dao => dao.Uuid == Uuid)).FirstOrDefault();

            if (topic == null) {
                return null;
            }
            return Mapper.MapTopicDaoToDto(topic);
        }

        public async Task<TopicDto> UpdateTopicHandler(Guid Uuid, TopicDto topicDto) {
            var topic = (await _topicsCollection.FindAsync(dao => dao.Uuid == Uuid)).FirstOrDefault();

            if (topic == null) {
                return null;
            }

            var filter = Builders<TopicDao>.Filter.Eq("Uuid", Uuid);
            var update = Builders<TopicDao>.Update.Set("Name", topicDto.Name);

            try{
                _topicsCollection.UpdateOne(filter, update);
            }catch {
                return null;
            }

            topicDto.Uuid = Uuid;
            return topicDto;
        }
    }
}