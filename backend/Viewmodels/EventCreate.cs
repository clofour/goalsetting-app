using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public abstract class EventCreate
    {
        [Required]
        public Guid? MovementId { get; set; }

        [Required]
        [MaxLength(FieldLimits.ShortText)]
        public string Name { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}