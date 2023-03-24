﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.Entities
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        public int AssignedFrom { get; set; }
        public int AssignedTo { get; set; }
        public DateTime AssignedOn { get; set; }
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }
        public DateTime DueDateTime { get; set; }
        public int? EscalatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
