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
    [Authorize]
    public class EventController(AppDbContext appDbContext, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create()
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
