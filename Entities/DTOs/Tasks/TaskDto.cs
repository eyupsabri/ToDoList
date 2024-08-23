using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums.DbEnums;

namespace Entities.DTOs.Tasks
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
