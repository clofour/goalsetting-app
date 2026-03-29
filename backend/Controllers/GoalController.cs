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
    //[ApiController]
    [AntiCSRF]
    public class GoalController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(new List<Goal>());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GoalForm goalForm)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid Model State: {@ModelState} {@GoalForm}", ModelState.Values, goalForm);
                return BadRequest(ModelState.Format());
            }

            return Ok();
        }
    }
}