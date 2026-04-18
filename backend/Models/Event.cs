using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum EventState {
        Complete,
        Incomplete,
        Unknown
    }

    public class Event()
    {

        public string Id { get; set; }
        public User User { get; set; }
        public Goal Goal { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public EventState EventState { get; set; }
        public Reflection? Reflection { get; set; }

    }
}
