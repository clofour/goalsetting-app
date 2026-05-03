using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Bearing : Goal
    {
        public NorthStar NorthStar { get; set; }

        public string Description { get; set; }
        public string Justification { get; set; }

        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }

        public List<Movement> Movements { get; set; }
    }
}
