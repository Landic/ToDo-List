using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Deadline { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = null!;
    }
}
