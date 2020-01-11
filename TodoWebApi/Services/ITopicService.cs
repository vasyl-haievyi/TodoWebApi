using System.Threading.Tasks;
using TodoWebApi.Dtos;
using System.Collections.Generic;
using System;

namespace TodoWebApi.Services {
    public interface ITopicService {
        Task<IEnumerable<TopicDto>> GetAllTopicsHandler();
        Task<TopicDto> GetTopicHandler(Guid Uuid);
        Task CreateTopicHandler(TopicDto topicDto);
        Task UpdateTopicHandler(Guid Uuid, TopicDto topicDto);
        Task DeleteTopicHandler(Guid Uuid);
    }
}