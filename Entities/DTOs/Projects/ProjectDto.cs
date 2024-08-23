using Entities.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Projects
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
