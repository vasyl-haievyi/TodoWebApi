using System.Collections.Generic;
using TodoWebApi.Dtos;
using TodoWebApi.Daos;
using System.Threading.Tasks;
using System;

namespace TodoWebApi.Services {
    public interface ITaskService {
        Task<IEnumerable<TodoTaskDto>> GetAllTasksHandler();
        Task<TodoTaskDto> GetTaskHandler(Guid Uuid);
        Task CreateTaskHandler(TodoTaskDto taskDto);
        Task<bool> UpdateTaskHandler(Guid Uuid, TodoTaskDto taskDto);
        Task<bool> DeleteTaskHandler(Guid Uuid);
    }
}