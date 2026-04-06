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
        public string name { get; set; }
        public GoalType goalType { get; set; }
    }
}