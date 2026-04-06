using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class BearingForm : GoalForm
    {
        public string parentId { get; set; }

        public string description { get; set; }
        public string justification { get; set; }

        public string? strengths { get; set; }
        public string? weaknesses { get; set; }
    }
}
