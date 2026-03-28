using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Reflection(User user, Event anEvent, string whatWorked, string whatDidntWork, string improvement)
    {
        public string ID;
        public User user = user;
        public Event anEvent = anEvent;

        public string whatWorked = whatWorked;
        public string whatDidntWork = whatDidntWork;
        public string improvement = improvement;
    }
}
