using System.ComponentModel.DataAnnotations;

namespace backend.Viewmodels
{
    public class GoalGet
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }
        public List<GoalGet> Children { get; set; }

        public string Name { get; set; }
    }
}