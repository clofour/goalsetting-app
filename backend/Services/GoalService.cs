using System.ComponentModel;
using backend.Data;
using backend.Enums;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class GoalService(AppDbContext appDbContext)
    {
        public IQueryable<Goal> ResolveGoalDbSet(GoalType goalType)
        {
            switch (goalType)
            {
                case GoalType.NorthStar:
                    return appDbContext.NorthStars;
                case GoalType.Bearing:
                    return appDbContext.Bearings;
                case GoalType.Movement:
                    return appDbContext.Movements;
            }

            throw new ArgumentOutOfRangeException(nameof(goalType));
        }

        public async Task<int> CountGoals<T>(User user) where T: Goal
        {
            return await appDbContext.Set<T>().CountAsync(goal => goal.User == user);
        }
    }
}