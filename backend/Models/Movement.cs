using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Movement : Goal
    {
        public Goal parent { get; set; }

        public MotivationType? motivationType { get; set; }
        public string? motivation { get; set; }
        public string? triggers { get; set; }
        public string? temptations { get; set; }
        public string? opts { get; set; }
        public string? obstacles { get; set; }
        public string killConditions { get; set; }
    }
}
