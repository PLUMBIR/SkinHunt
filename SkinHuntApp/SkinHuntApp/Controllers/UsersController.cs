using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Commands;
using SkinHunt.Application.Common.Models;
using SkinHunt.Application.Queries;

namespace SkinHunt.Service.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : AppControllerBase 
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpPost("update-avatar")]
        public async Task<IActionResult> UpdateUserAvatar([FromBody] UpdateUserAvatarModel model)
        {
            try
            {
                var result = await _mediator.Send(new UpdateUserAvatarCommand(model));

                if (result is not null)
                {
                    _logger.LogInformation("User avatar updated.");
                    return Ok(result);
                }

                _logger.LogError("Update user avatar failed.");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during update avatar");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            } 
        }

        [HttpPost("buy-skin")]
        public async Task<IActionResult> BuySkin([FromBody] SoldModel model)
        {
            try
            {
                var result = await _mediator.Send(new BuySkinCommand(model));

                if (result)
                {
                    _logger.LogError("Skin purchased successfully");
                    return Ok(result);
                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during purchase skin");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserBalance(string Id)
        {
            try
            {
                var result = await _mediator.Send(new GetUserBalanceQuery(Id));

                _logger.LogInformation("User balance updated.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during update avatar");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
