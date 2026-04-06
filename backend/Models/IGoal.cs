using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public interface IGoal()
    {
        public string ID { get; set; }
        public User user { get; set; }

        public string name { get; set; }
        public string description { get; set; }

        public GoalType goalType { get; set; }
        public Goal parent { get; set; }

        public string specific { get; set; }
        public string measurable { get; set; }
        public string attainable { get; set; }
        public string realistic { get; set; }
        public string timeBound { get; set; }

        public string why { get; set; }
        public GoalImportance importance { get; set; }
        public string difficulty { get; set; }
        public string strengths { get; set; }
        public string weaknesses { get; set; }
        
        public string obstacles { get; set; }
        public MotivationType motivationType { get; set; }
        public string motivation { get; set; }

        public string backupPlan { get; set; }

        public string killConditions { get; set; }
    }
}
