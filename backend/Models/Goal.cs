using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models
{
    public abstract class Goal
    {
        public Goal? Parent { get; set; }
        public List<Movement> Children { get; set; }

        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
