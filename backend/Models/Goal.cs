using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public enum GoalType {
        Superordinate,
        Intermediate,
        Subordinate
    }

    public enum GoalImportance {
        High,
        None
    }

    public enum MotivationType {
        Carrot,
        Stick
    }

    public class Goal(User user, string name, string description, GoalType goalType, Goal parent, string specific, string measurable, string attainable, string realistic, string timeBound, string why, GoalImportance importance, string difficulty, string strengths, string weaknesses, string obstacles, MotivationType motivationType, string motivation, string backupPlan, string killConditions)
    {
        public string ID;
        public User user = user;

        public string name = name;
        public string description = description;

        public GoalType goalType = goalType;
        public Goal parent = parent;

        public string specific = specific;
        public string measurable = measurable;
        public string attainable = attainable;
        public string realistic = realistic;
        public string timeBound = timeBound;

        public string why = why;
        public GoalImportance importance = importance;
        public string difficulty = difficulty;
        public string strengths = strengths;
        public string weaknesses = weaknesses;
        
        public string obstacles = obstacles;
        public MotivationType motivationType = motivationType;
        public string motivation = motivation;

        public string backupPlan = backupPlan;

        public string killConditions = killConditions;
    }
}
