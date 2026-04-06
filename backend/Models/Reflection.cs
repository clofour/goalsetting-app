using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Reflection()
    {
        public Guid Id { get; set; }
        public User user { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }

        public string whatWorked { get; set; }
        public string whatDidntWork { get; set; }
        public string improvement { get; set; }
    }
}
