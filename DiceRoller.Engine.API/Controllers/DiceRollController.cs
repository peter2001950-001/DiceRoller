using DiceRoller.Engine.API.Models.Requests;
using DiceRoller.Engine.API.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Engine.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DiceRollController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDiceRollService _diceRollService;
        public DiceRollController(IUserService userService, IDiceRollService diceRollService)
        {
            _userService = userService;
            _diceRollService = diceRollService;
        }

        [HttpPost("roll")]
        public async Task<IActionResult> RollDiceAsync()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return new ForbidResult();
            }

            var result = await _diceRollService.RollTheDiceAsync(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetDiceRollRequest request)
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return new ForbidResult();
            }

            var result = await _diceRollService.GetAllPaginatedAsync(user, request);
            return Ok(result);
        }
    }
}
