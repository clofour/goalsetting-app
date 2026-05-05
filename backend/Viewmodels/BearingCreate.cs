using System.ComponentModel.DataAnnotations;
using backend.Config;

namespace backend.Viewmodels
{
    public class BearingCreate : GoalCreate
    {
        public Guid NorthStarId { get; set; }

        [MaxLength(FieldLimits.LongText)]
        public string Description { get; set; }
        [MaxLength(FieldLimits.LongText)]
        public string Justification { get; set; }

        [MaxLength(FieldLimits.LongText)]
        public string? Strengths { get; set; }
        [MaxLength(FieldLimits.LongText)]
        public string? Weaknesses { get; set; }
    }
}
