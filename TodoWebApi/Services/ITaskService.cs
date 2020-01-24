using System.Collections.Generic;
using TodoWebApi.Dtos;
using TodoWebApi.Daos;
using System.Threading.Tasks;
using System;

namespace TodoWebApi.Services {
    public interface ITaskService {
        Task<IEnumerable<TodoTaskDto>> GetAllTasksHandler();
        Task<TodoTaskDto> GetTaskHandler(Guid Uuid);
        Task<TodoTaskDto> CreateTaskHandler(TodoTaskDto taskDto);
        Task<TodoTaskDto> UpdateTaskHandler(Guid Uuid, TodoTaskDto taskDto);
        Task<TodoTaskDto> DeleteTaskHandler(Guid Uuid);
    }
}