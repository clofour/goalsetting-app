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

        public string ID { get; set; }
        public User user { get; set; }
        public Goal goal { get; set; }

        public string name { get; set; }

        public DateTime start { get; set; }
        public DateTime end { get; set; }
        
        public EventState eventState { get; set; }
        public Reflection? reflection { get; set; }

    }
}
