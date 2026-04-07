using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Viewmodels
{
    public class NorthStarCreate: GoalCreate
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public GoalImportance Importance { get; set; }
        public string Justification { get; set; }
    }
}