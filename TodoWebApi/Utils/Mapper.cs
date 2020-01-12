using TodoWebApi.Daos;
using TodoWebApi.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace TodoWebApi.Utils {
    public static class Mapper {
        public static TopicDao MapTopicDtoToDao(TopicDto dto) {
            return new TopicDao() {
                Name = dto.Name
            };
        }

        public static TopicDto MapTopicDaoToDto(TopicDao dao, IEnumerable<TodoTaskDto> taskDtos) {
            return new TopicDto() {
                Name = dao.Name,
                Tasks = taskDtos
            };
        }

        public static TodoTaskDao MapTaskDtoToDao(TodoTaskDto taskDto) {
            return new TodoTaskDao {
                Name = taskDto.Name,
                Deadline = taskDto.Deadline,
                TopicUuid = taskDto.TopicGuid
            };
        }

        public static TodoTaskDto MapTaskDaoToDto(TodoTaskDao taskDao) {
            return new TodoTaskDto() {
                Name = taskDao.Name,
                Deadline = taskDao.Deadline,
                TopicGuid = taskDao.TopicUuid
            };
        }
    }
}