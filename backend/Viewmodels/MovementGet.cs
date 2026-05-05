using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Viewmodels
{
    public class MovementGet : GoalGet
    {
        public Guid BearingId { get; set; }

        public MotivationType? MotivationType { get; set; }
        public string? Motivation { get; set; }
        public string? Triggers { get; set; }
        public string? Temptations { get; set; }
        public string? Obstacles { get; set; }
        public string? KillConditions { get; set; }
    }
}
