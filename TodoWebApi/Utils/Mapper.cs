using TodoWebApi.Daos;
using TodoWebApi.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace TodoWebApi.Utils {
    public static class Mapper {
        public static TopicDao MapTopicDtoToDao(TopicDto dto) {
            return new TopicDao() {
                Name = dto.Name,
                Uuid = dto.Uuid
            };
        }

        public static TopicDto MapTopicDaoToDto(TopicDao dao) {
            return new TopicDto() {
                Name = dao.Name,
                Uuid = dao.Uuid
            };
        }

        public static TodoTaskDao MapTaskDtoToDao(TodoTaskDto taskDto) {
            return new TodoTaskDao {
                Name = taskDto.Name,
                Deadline = taskDto.Deadline,
                TopicUuid = taskDto.TopicUuid,
                Uuid = taskDto.Uuid
            };
        }

        public static TodoTaskDto MapTaskDaoToDto(TodoTaskDao taskDao) {
            return new TodoTaskDto() {
                Name = taskDao.Name,
                Deadline = taskDao.Deadline,
                TopicUuid = taskDao.TopicUuid,
                Uuid = taskDao.Uuid
            };
        }
    }
}