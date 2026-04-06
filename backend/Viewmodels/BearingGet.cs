using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class BearingGet : GoalGet
    {
        public Guid ParentId { get; set; }

        public string Description { get; set; }
        public string Justification { get; set; }

        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }

        public List<MovementGet> Movements { get; set; }
    }
}