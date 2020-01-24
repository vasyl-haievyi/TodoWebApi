using System.Threading.Tasks;
using TodoWebApi.Dtos;
using System.Collections.Generic;
using System;

namespace TodoWebApi.Services {
    public interface ITopicService {
        Task<IEnumerable<TopicDto>> GetAllTopicsHandler();
        Task<TopicDto> GetTopicHandler(Guid Uuid);
        Task<TopicDto> CreateTopicHandler(TopicDto topicDto);
        Task<TopicDto> UpdateTopicHandler(Guid Uuid, TopicDto topicDto);
        Task<TopicDto> DeleteTopicHandler(Guid Uuid);
    }
}