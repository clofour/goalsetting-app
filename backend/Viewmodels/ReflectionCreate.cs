using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class ReflectionCreate
    {
        public Guid EventId { get; set; }

        public string Positive { get; set; }
        public string Negative { get; set; }
        public string Improvement { get; set; }
    }
}