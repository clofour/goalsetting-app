using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum EventState {
        Complete,
        Incomplete,
        Unknown
    }

    public class Event(string name, int uses)
    {
        public string ID = id;
        public User user = user;
        public Goal goal = goal;

        public string name = name;

        public DateTime start = start;
        public DateTime end = end;
        
        public EventState eventState = eventState;
        public Reflection? reflection = reflection;
    }
}
