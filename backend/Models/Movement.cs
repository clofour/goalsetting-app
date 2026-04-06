using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum MotivationType
    {
        Carrot,
        Stick
    }

    public class Movement : Goal
    {
        public Goal Parent { get; set; }

        public MotivationType? MotivationType { get; set; }
        public string? Motivation { get; set; }
        public string? Triggers { get; set; }
        public string? Temptations { get; set; }
        public string? Opts { get; set; }
        public string? Obstacles { get; set; }
        public string? KillConditions { get; set; }
    }
}
