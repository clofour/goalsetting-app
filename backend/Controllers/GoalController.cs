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
using System.Collections.ObjectModel;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class GoalController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<GoalController> logger, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<NorthStarGet>), StatusCodes.Status200OK)]
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

        [HttpPost]
        public async Task<ActionResult> CreateNorthStar([FromBody] NorthStarCreate northStarCreate)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid Model State: {@ModelState} {@GoalForm}", ModelState.Values, northStarCreate);
                return BadRequest(ModelState.Format());
            }

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
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid Model State: {@ModelState} {@GoalForm}", ModelState.Values, bearingCreate);
                return BadRequest(ModelState.Format());
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            NorthStar? parent = await appDbContext.Goals.FindAsync(bearingCreate.ParentId) as NorthStar;
            if (parent == null || parent.User != user)
            {
                return NotFound("The parent goal does not exist.");
            }

            Bearing bearing = new Bearing();
            mapper.Map(bearingCreate, bearing);

            parent.Bearings.Add(bearing);

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovement([FromBody] MovementCreate movementCreate)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid Model State: {@ModelState} {@GoalForm}", ModelState.Values, movementCreate);
                return BadRequest(ModelState.Format());
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            Bearing? parent = await appDbContext.Goals.FindAsync(movementCreate.ParentId) as Bearing;
            if (parent == null || parent.User != user)
            {
                return NotFound("The parent goal does not exist.");
            }

            Movement movement = new Movement();
            mapper.Map(movementCreate, movement);

            parent.Movements.Add(movement);

            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string ID)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid();
            }

            Goal? goal = await appDbContext.Goals.FindAsync(ID);
            if (goal != null && goal.User == user)
            {
                return Ok();
            }

            return Ok();
        }
    }
}