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
        public string Description { get; set; }
        public GoalImportance Importance { get; set; }
        public string Justification { get; set; }
    }
}
