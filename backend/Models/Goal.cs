using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public abstract class Goal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public GoalType Type { get; set; }
    }
}
