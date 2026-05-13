using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Config;

namespace backend.Models
{
    public enum EventState
    {
        Complete,
        Incomplete,
        Unknown
    }

    public class EventInstanceState()
    {
        [Key]
        public string Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public EventState EventState { get; set; }
        public Guid ReflectionId { get; set; }
        [ForeignKey("ReflectionId")]
        public Reflection Reflection { get; set; }
    }
}
