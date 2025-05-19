using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Task
{
    public class TaskUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
