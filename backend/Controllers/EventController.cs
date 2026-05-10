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


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class EventController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger, IMapper mapper, EventService eventService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<EventGet>), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> Get()
        {
            User? user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            List<Event> events = await appDbContext.Events
                .Where(e => e.UserId == user.Id)
                .ToListAsync();
            
            List<EventGet> eventsGet = new List<EventGet>();
            mapper.Map(events, eventsGet);

            return Ok(eventsGet);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> CreateOnetime([FromBody] OnetimeEventCreate onetimeEventCreate)
        {
            User? user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            OnetimeEvent onetimeEvent = new OnetimeEvent();
            mapper.Map(onetimeEventCreate, onetimeEvent);
            onetimeEvent.End = eventService.ConstructEnd(onetimeEventCreate.Start, onetimeEventCreate.Duration);
            onetimeEvent.User = user;

            appDbContext.Events.Add(onetimeEvent);
            await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK, "application/json")]
        public async Task<ActionResult> CreateRecurring([FromBody] RecurringEventCreate recurringEventCreate)
        {
            User? user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            RecurringEvent recurringEvent = new RecurringEvent();
            mapper.Map(recurringEventCreate, recurringEvent);
            recurringEvent.RRULE = eventService.ConstructRRULE(recurringEventCreate);
            recurringEvent.End = eventService.ConstructEnd(recurringEventCreate.Start, recurringEventCreate.Duration);
            recurringEvent.User = user;

            appDbContext.Events.Add(recurringEvent);
            await appDbContext.SaveChangesAsync();

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
