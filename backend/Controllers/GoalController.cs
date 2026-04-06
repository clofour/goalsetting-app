using backend.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using backend.Filters;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AntiCSRF]
    [Authorize]
    public class GoalController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<GoalController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid();
            }

            return Ok(user.Goals);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Goal goal) // TODO: Use a viewmodel
        {
            Console.Write(goal);

            // if (!ModelState.IsValid)
            // {
            //     logger.LogWarning("Invalid Model State: {@ModelState} {@GoalForm}", ModelState.Values, goalForm);
            //     return BadRequest(ModelState.Format());
            // }

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
            if (goal != null && goal.user == user)
            {
                return Ok();
            }

            return Ok();
        }
    }
}