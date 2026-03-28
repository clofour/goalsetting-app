using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum EventState {
        Complete,
        Incomplete,
        Unknown
    }

    public class Event(User user, Goal goal, string name, DateTime startTime, DateTime endTime, EventState eventState, Reflection? reflection)
    {

        public string ID;
        public User user = user;
        public Goal goal = goal;

        public string name = name;

        public DateTime start = startTime;
        public DateTime end = endTime;
        
        public EventState eventState = eventState;
        public Reflection? reflection = reflection;
    }
}
