using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public enum GoalImportance
    {
        High,
        None
    }

    public class NorthStarForm: GoalForm
    {
        public string description { get; set; }
        public GoalImportance importance { get; set; }
        public string justification { get; set; }
    }
}