using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class ReflectionGet
    {
        [Required]
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Positive { get; set; }
        public string Negative { get; set; }
        public string Improvement { get; set; }
    }
}