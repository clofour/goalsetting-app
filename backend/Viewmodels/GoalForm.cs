using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public enum GoalType {
        Superordinate,
        Intermediate,
        Subordinate
    }

    public abstract class GoalForm
    {
        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}