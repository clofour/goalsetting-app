using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models
{
    public class NorthStar : Goal
    {
        public string Description { get; set; }
        public GoalImportance Importance { get; set; }
        public string Justification { get; set; }
        // public List<Bearing> Children { get; set; }
    }
}
