using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApi.Dtos;

namespace TodoWebApi.Services.Impl {
    public class TopicServiceImpl : ITopicService
    {
        public Task CreateTopicHandler(TopicDto topicDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopicHandler(Guid Uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TopicDto>> GetAllTopicsHandler()
        {
            throw new NotImplementedException();
        }

        public Task<TopicDto> GetTopicHandler(Guid Uuid)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopicHandler(Guid Uuid, TopicDto topicDto)
        {
            throw new NotImplementedException();
        }
    }
}