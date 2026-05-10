using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public class MovementGet : GoalGet
    {
        [Required]
        public Guid BearingId { get; set; }

        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? Difficulty { get; set; }
        [Required]
        public MotivationType? MotivationType { get; set; }
        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? Motivation { get; set; }
        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? Triggers { get; set; }
        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? Temptations { get; set; }
        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? Obstacles { get; set; }
        [Required]
        [MaxLength(FieldLimits.LongText)]
        public string? KillConditions { get; set; }
    }
}
