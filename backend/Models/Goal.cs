using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum GoalType {
        Superordinate,
        Intermediate,
        Subordinate
    }

    public enum MotivationType {
        Carrot,
        Stick
    }

    public abstract class Goal
    {
        public string ID { get; set; }
        public User user { get; set; }

        public string name { get; set; }
        public GoalType goalType { get; set; }
    }
}
