using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public enum MotivationType
    {
        Carrot,
        Stick
    }

    public class MovementForm : GoalForm
    {
        public Guid ParentID { get; set; }

        public MotivationType? MotivationType { get; set; }
        public string? Motivation { get; set; }
        public string? Triggers { get; set; }
        public string? Temptations { get; set; }
        public string? Opts { get; set; }
        public string? Obstacles { get; set; }
        public string? KillConditions { get; set; }
    }
}