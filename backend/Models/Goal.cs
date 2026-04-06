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
        public User user { get; set; }

        public string name { get; set; }
        public GoalType goalType { get; set; }
    }
}
