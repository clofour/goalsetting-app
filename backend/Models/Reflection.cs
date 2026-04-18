using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Reflection
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }

        public string WhatWorked { get; set; }
        public string WhatDidntWork { get; set; }
        public string Improvement { get; set; }
    }
}
