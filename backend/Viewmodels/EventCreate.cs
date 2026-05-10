using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public abstract class EventCreate
    {
        public Guid? MovementId { get; set; }

        [MaxLength(FieldLimits.ShortText)]
        public string Name { get; set; }

        public DateTime Start { get; set; }
        public int Duration { get; set; }
    }
}