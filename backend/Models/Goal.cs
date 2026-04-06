using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum GoalType {
        NorthStar,
        Bearing,
        Movement
    }

    public abstract class Goal
    {
        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
