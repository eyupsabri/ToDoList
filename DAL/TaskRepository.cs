using Dapper;
using Entities;
using Entities.DTOs.Projects;
using Entities.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaskRepository : ITaskRepository
    {
        private DapperContext _dapperContext;
        public TaskRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<List<TaskDto>> GetAllTasks(int projectId)
        {
            var query = @"
              SELECT 
               TaskId
              ,TaskName
              ,t.Description
              ,Status
              ,Priority
              ,DueDate
              ,p.ProjectName
              ,us.Username as 'CreatedBy'
	          ,u.Username as 'AssignedTo'
              , p.ProjectName
          FROM Tasks as t
          LEFT OUTER JOIN Projects as p ON p.ProjectId = t.ProjectId
          INNER JOIN Users as u ON t.AssignedTo = u.UserId
          INNER JOIN Users as us ON t.CreatedBy = us.UserId
          WHERE t.ProjectId = @projectId";
            var parameters = new DynamicParameters();
            parameters.Add("@projectId", projectId);

            using (var connection = _dapperContext.CreateConnection())
            {
                var projects = await connection.QueryAsync<TaskDto>(query, parameters);
                return projects.ToList();
            }
        }

        public async Task<bool> UpdateTaskStatus(TaskStatusUpdateDto task)
        {
            var query = @"
                UPDATE Tasks
                    SET Status = @status
                    WHERE TaskId = @taskId;
            ";
            var parameters = new DynamicParameters();
            parameters.Add("@status", task.Status);
            parameters.Add("@taskId", task.TaskId);
            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);
                return result > 0;
            }
        }
    }
}
