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
    public class TaskServiceImpl : ITaskService {
        private readonly IMongoCollection<TodoTaskDao> _tasksCollection;
        private readonly DatabaseSettings _databaseSetings;

        public TaskServiceImpl(IOptions<DatabaseSettings> opSetting) {
            _databaseSetings = opSetting.Value;

            var client = new MongoClient(_databaseSetings.ConnectionString);
            var database = client.GetDatabase(_databaseSetings.TodoDatabase);

            _tasksCollection = database.GetCollection<TodoTaskDao>(_databaseSetings.TaskCollection);
        }
        public async Task<TodoTaskDto> CreateTaskHandler(TodoTaskDto taskDto) {
            var taskDao = Mapper.MapTaskDtoToDao(taskDto);
            taskDao.Uuid = Guid.NewGuid();
            
            try {
                await _tasksCollection.InsertOneAsync(taskDao);
            }catch {
                return null;
            }

            taskDto.Uuid = taskDao.Uuid;

            return taskDto;
        }

        public async Task<TodoTaskDto> DeleteTaskHandler(Guid Uuid) {
            var taskDao = (await _tasksCollection.FindAsync(taskDao => taskDao.Uuid == Uuid)).FirstOrDefault();

            if(taskDao == null) {
                return null;
            }

            try {
                await _tasksCollection.DeleteOneAsync(taskDao => taskDao.Uuid == Uuid);
            }catch {
                return null;
            }

            return Mapper.MapTaskDaoToDto(taskDao);
        }

        public async Task<IEnumerable<TodoTaskDto>> GetAllTasksHandler() {
            return (await _tasksCollection.FindAsync(taskDao => true))
            .ToList()
            .Select(taskDao => Mapper.MapTaskDaoToDto(taskDao));
        }

        public async Task<TodoTaskDto> GetTaskHandler(Guid Uuid) {
            var taskDao = (await _tasksCollection.FindAsync(taskDao => taskDao.Uuid == Uuid)).FirstOrDefault();

            if(taskDao == null) {
                return null;
            }

            return Mapper.MapTaskDaoToDto(taskDao);
        }

        public async Task<TodoTaskDto> UpdateTaskHandler(Guid Uuid, TodoTaskDto taskDto) {
            var taskDao = (await _tasksCollection.FindAsync(taskDao => taskDao.Uuid == Uuid)).FirstOrDefault();

            if (taskDao == null) {
                return null;
            }

            var filter = Builders<TodoTaskDao>.Filter.Eq("Uuid", Uuid);
            var update = Builders<TodoTaskDao>.Update
            .Set("Name", taskDto.Name)
            .Set("Deadline", taskDto.Deadline)
            .Set("TopicUuid", taskDto.TopicUuid);

            UpdateResult result = null;
            try {
                result = _tasksCollection.UpdateOne(filter, update);
            } catch {
                return null;
            }

            if (!result.IsAcknowledged) {
                return null;
            }

            taskDto.Uuid = taskDao.Uuid;

            return taskDto;
        }
    }
}