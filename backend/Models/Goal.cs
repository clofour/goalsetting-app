using System.ComponentModel.DataAnnotations;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public abstract class Goal
    {
        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
