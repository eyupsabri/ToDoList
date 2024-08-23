using DAL;
using Entities.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<TaskDto>> GetAllTasks(int projectId)
        {
            var tasks = await _taskRepository.GetAllTasks(projectId);
            return tasks;
        }
    }
}
