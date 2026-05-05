using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Config;

namespace backend.Models
{
    public enum EventState {
        Complete,
        Incomplete,
        Unknown
    }

    public class Event()
    {
        [Key]
        public string Id { get; set; }
        public Guid UserId { get; set; }
         [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid BearingId { get; set; }
        [ForeignKey("BearingId")]
        public Movement Movement { get; set; }

        [MaxLength(FieldLimits.ShortText)]
        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public EventState EventState { get; set; }
        public Reflection? Reflection { get; set; }

    }
}
