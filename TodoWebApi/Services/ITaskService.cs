using System.Collections.Generic;
using TodoWebApi.Dtos;
using TodoWebApi.Daos;
using System.Threading.Tasks;
using System;

namespace TodoWebApi.Services {
    public interface ITaskService {
        Task<IEnumerable<TodoTaskDto>> GetAllTasksHandler();
        Task<IEnumerable<TodoTaskDto>> GetAllTasksFromTopicHandler(Guid TopicUuid);
        Task<TodoTaskDao> GetTaskHandler(Guid Uuid);
        Task CreateTaskHandler(TodoTaskDto taskDto);
        Task UpdateTaskHandler(Guid Uuid, TodoTaskDto taskDto);
        Task DeleteTaskHandler(Guid Uuid);
        Task DeleteAllTasksFromTopicHandler(Guid Uuid);

    }
}