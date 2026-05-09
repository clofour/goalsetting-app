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


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class EventController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> CreateOnetime([FromBody] OnetimeEventCreate onetimeEventCreate)
        {
            User user = await userManager.GetUserAsync(User);

            OnetimeEvent onetimeEvent = new OnetimeEvent();
            mapper.Map(onetimeEventCreate, onetimeEvent);
            onetimeEvent.User = user;

            appDbContext.Events.Add(onetimeEvent);
            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> CreateRecurring([FromBody] RecurringEventCreate recurringEventCreate)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Update()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}
