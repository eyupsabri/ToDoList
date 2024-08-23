using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public static class DbEnums
    {
        public enum Priority
        {
            Low,
            Medium,
            High
        }
        public enum Status
        {
            InProgress,
            ToDo,
            Done

        }
    }
}
