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
        public EventState EventState { get; set; }
        Guid ReflectionId { get; set; }
        [ForeignKey("ReflectionId")]
        Reflection Reflection { get; set; }
    }
}
