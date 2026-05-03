using System.ComponentModel.DataAnnotations;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    [Index(nameof(User))]
    public abstract class Goal
    {
        public Goal? Parent { get; set; }

        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
