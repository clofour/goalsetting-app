using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum GoalImportance
    {
        High,
        None
    }

    public class NorthStar : Goal
    {
        public string description { get; set; }
        public GoalImportance importance { get; set; }
        public string justification { get; set; }
    }
}
