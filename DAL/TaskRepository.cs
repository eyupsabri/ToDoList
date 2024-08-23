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
              ,Description
              ,Status
              ,Priority
              ,DueDate
              ,ProjectId
              ,us.Username as 'CreatedBy'
	          ,u.Username as 'AssignedTo'
          FROM Tasks as t
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
    }
}
