using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Viewmodels
{
    public abstract class GoalCreate
    {
        public Guid ParentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}