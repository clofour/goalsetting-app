using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public class NorthStarCreate: GoalCreate
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(FieldLimits.ShortText)]
        public string Description { get; set; }
        public GoalImportance Importance { get; set; }
        [MaxLength(FieldLimits.LongText)]
        public string Justification { get; set; }
    }
}