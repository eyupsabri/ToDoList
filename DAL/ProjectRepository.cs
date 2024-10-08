﻿using Dapper;
using Entities;
using Entities.DTOs.Projects;
using Entities.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DapperContext _dapperContext;
        public ProjectRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        //public async Task<List<Task>> GetProjectByProjectId(int projectId)
        //{
        //    string query = @"SELECT p.ProjectName,
        //      t.TaskId, t.TaskName, t.Status,t.Description, t.Priority, t.DueDate, t.CreatedAt, u.Username, us.Username
        //      From Projects as p
        //      INNER JOIN Tasks as t ON p.ProjectId=t.ProjectId 
        //      INNER JOIN Users as u ON t.CreatedBy = u.UserId
        //      INNER JOIN Users as us ON t.AssignedTo = us.UserId
        //      WHERE p.ProjectId = @projectId";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@projectId", projectId);

        //    using (var connection = _dapperContext.CreateConnection())
        //    {
        //        var tasks = await connection.QueryAsync<Task>(query, parameters);
        //        return tasks.ToList();
        //    }
        //}

        public async Task<List<ProjectDto>> GetProjectsForUser(string email)
        {
            string query = @"
                SELECT 
                    p.ProjectId, 
                    p.ProjectName, 
                    p.Description, 
                    us.FullName AS 'CreatedBy', 
                    p.CreatedAt 
                FROM 
                    Users AS u
                INNER JOIN 
                    ProjectMembers AS pm ON u.UserId = pm.UserId
                INNER JOIN 
                    Projects AS p ON pm.ProjectId = p.ProjectId
                INNER JOIN 
                    Users AS us ON p.CreatedBy = us.UserId
                WHERE 
                    u.Email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            using (var connection = _dapperContext.CreateConnection())
            {
                var projects = await connection.QueryAsync<ProjectDto>(query, parameters);
                return projects.ToList();
            }

        }
    }
}
