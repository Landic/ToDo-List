using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Task
{
    public class TaskCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int UserId { get; set; }
    }
}
