using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Viewmodels
{
    public class NorthStarGet : GoalGet
    {
        public string Description { get; set; }
        public GoalImportance Importance { get; set; }
        public string Justification { get; set; }
    }
}