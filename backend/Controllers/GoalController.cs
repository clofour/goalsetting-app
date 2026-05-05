using backend.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using backend.Filters;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using backend.Services;
using backend.Enums;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class GoalController(AppDbContext appDbContext, UserManager<User> userManager, ILogger<GoalController> logger, IMapper mapper, GoalService goalService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<NorthStarGet>), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> Get()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            await appDbContext.Entry(user)
                .Collection(user => user.Goals)
                .Query()
                .Include(northStar => northStar.Bearings)
                    .ThenInclude(bearing => bearing.Movements)
                .LoadAsync();

            List<NorthStar> goals = user.Goals;

            List<NorthStarGet> goalsDTO = mapper.Map<List<NorthStarGet>>(goals);

            return Ok(goalsDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Stats()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            GoalStats goalStats = new GoalStats
            {
                NorthStarCount = await goalService.CountGoals<NorthStar>(user),
                BearingCount = await goalService.CountGoals<Bearing>(user),
                MovementCount = await goalService.CountGoals<Movement>(user)
            };

            return Ok(goalStats);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNorthStar([FromBody] NorthStarCreate northStarCreate)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            NorthStar northStar = new NorthStar();
            mapper.Map(northStarCreate, northStar);

            user.Goals.Add(northStar);

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBearing([FromBody] BearingCreate bearingCreate)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            NorthStar? parent = await appDbContext.NorthStars.FindAsync(bearingCreate.NorthStarId);
            if (parent == null || parent.User != user)
            {
                return NotFound("The parent goal does not exist.");
            }

            await appDbContext.Entry(parent).Collection(northStar => northStar.Bearings).LoadAsync();

            Bearing bearing = new Bearing();
            mapper.Map(bearingCreate, bearing);

            bearing.User = user;
            bearing.NorthStar = parent;
            parent.Bearings.Add(bearing);

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovement([FromBody] MovementCreate movementCreate)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            Bearing? parent = await appDbContext.Bearings.FindAsync(movementCreate.BearingId);
            if (parent == null || parent.User != user)
            {
                return NotFound("The parent goal does not exist.");
            }

            await appDbContext.Entry(parent).Collection(bearing => bearing.Movements).LoadAsync();

            Movement movement = new Movement();
            mapper.Map(movementCreate, movement);

            movement.User = user;
            movement.Bearing = parent;
            parent.Movements.Add(movement);

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, GoalType goalType)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            await goalService.ResolveGoalDbSet(goalType)
                .Where(goal => goal.Id == id && goal.User == user)
                .ExecuteDeleteAsync();

            await appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}