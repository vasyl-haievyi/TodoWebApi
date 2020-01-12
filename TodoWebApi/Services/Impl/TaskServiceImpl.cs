using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoWebApi.Daos;
using TodoWebApi.Dtos;
using TodoWebApi.Properties;
using TodoWebApi.Utils;

namespace TodoWebApi.Services.Impl {
    public class TaskServiceImpl : ITaskService {
        private readonly IMongoCollection<TodoTaskDao> _tasksCollection;
        private readonly DatabaseSettings _databaseSetings;

        public TaskServiceImpl(DatabaseSettings settings) {
            _databaseSetings = settings;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(_databaseSetings.TodoDatabase);

            _tasksCollection = database.GetCollection<TodoTaskDao>(_databaseSetings.TaskCollection);
        }
        public async Task CreateTaskHandler(TodoTaskDto taskDto) {
            var taskDao = Mapper.MapTaskDtoToDao(taskDto);
            taskDao.Uuid = Guid.NewGuid();

            await _tasksCollection.InsertOneAsync(taskDao);
        }

        public async Task<bool> DeleteTaskHandler(Guid Uuid) {
            var taskDao = await _tasksCollection.FindAsync(taskDao => taskDao.Uuid == Uuid);

            if(taskDao == null) {
                return false;
            }

            await _tasksCollection.DeleteOneAsync(taskDao => taskDao.Uuid == Uuid);

            return true;
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

        public async Task<bool> UpdateTaskHandler(Guid Uuid, TodoTaskDto taskDto) {
            var taskDao = (await _tasksCollection.FindAsync(taskDao => taskDao.Uuid == Uuid)).FirstOrDefault();

            if (taskDao == null) {
                return false;
            }

            var filter = Builders<TodoTaskDao>.Filter.Eq("Uuid", Uuid);
            var update = Builders<TodoTaskDao>.Update
            .Set("name", taskDto.Name)
            .Set("deadline", taskDto.Deadline)
            .Set("topic_guid", taskDto.TopicGuid);

            _tasksCollection.UpdateOne(filter, update);

            return true;
        }
    }
}