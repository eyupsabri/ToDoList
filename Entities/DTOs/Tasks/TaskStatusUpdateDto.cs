using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums.DbEnums;

namespace Entities.DTOs.Tasks
{
    public class TaskStatusUpdateDto
    {
        public int TaskId { get; set; }
        public Status Status { get; set; }
    }
}
