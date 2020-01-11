using System.Threading.Tasks;
using TodoWebApi.Dtos;
using System.Collections.Generic;
using System;

namespace TodoWebApi.Services {
    public interface ITopicService {
        Task<IEnumerable<TopicDto>> GetAllTopicsHandler();
        Task<TopicDto> GetTopicHandler(Guid Id);
        Task CreateTopicHandler(TopicDto topicDto);
        Task UpdateTopicHandler(Guid Id, TopicDto topicDto);
        Task DeleteTopicHandler(Guid Id);
    }
}