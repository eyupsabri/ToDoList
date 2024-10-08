﻿using Entities.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ITaskService
    {
        public Task<List<TaskDto>> GetAllTasks(int projectId);
        public Task<bool> UpdateTaskStatus(TaskStatusUpdateDto task);
    }
}
