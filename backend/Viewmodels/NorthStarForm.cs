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
        public string Description { get; set; }
        public GoalImportance Importance { get; set; }
        public string Justification { get; set; }
    }
}