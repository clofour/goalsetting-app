using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class GoalService(AppDbContext appDbContext)
    {
        public async Task<int> CountGoals<T>(User user) where T: Goal
        {
            return await appDbContext.Set<T>().CountAsync(goal => goal.User == user);
        }
    }
}