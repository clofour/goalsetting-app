using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Bearing : Goal
    {
        public Goal parent { get; set; }

        public string description { get; set; }
        public string justification { get; set; }

        public string? strengths { get; set; }
        public string? weaknesses { get; set; }
    }
}
