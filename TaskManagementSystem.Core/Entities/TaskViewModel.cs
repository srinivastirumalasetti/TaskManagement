using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Entities
{
    public class TaskViewModel
    {
        public int TaskID { get; set; }
        public string AssignedFrom { get; set; }
        public string AssignedTo { get; set; }
        public DateTime AssignedOn { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDateTime { get; set; }

        public int? EscalatedBy { get; set; } = 0;
        public string EscalatedUser { get; set; } = "";
        public int DateFlagValue { get; set; }

    }

}
