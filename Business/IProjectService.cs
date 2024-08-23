using Entities.DTOs.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllProjectsForUser(string email);
    }
}
