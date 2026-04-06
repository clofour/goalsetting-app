using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models
{
    public abstract class Goal
    {
        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
