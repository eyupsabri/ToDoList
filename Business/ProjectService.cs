using Entities.DTOs.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _repository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _repository = projectRepository;
        }
        public async Task<List<ProjectDto>> GetAllProjectsForUser(string email)
        {
            var projects = await _repository.GetProjectsForUser(email);
            return projects;
        }
    }
}
