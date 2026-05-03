using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class GoalStats
    {
        public int NorthStarCount { get; set; }
        public int BearingCount { get; set; }
        public int MovementCount { get; set; }
    }
}