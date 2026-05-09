using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Reflection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string ReflectionId { get; set; }
        [ForeignKey("ReflectionId")]
        public Event Event { get; set; }

        public string Positive { get; set; }
        public string Negative { get; set; }
        public string Improvement { get; set; }
    }
}
