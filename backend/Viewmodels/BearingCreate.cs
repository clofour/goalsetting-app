using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class BearingCreate : GoalCreate
    {
        public Guid NorthStarId { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }

        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }
    }
}
