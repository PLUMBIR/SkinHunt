using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Commands;
using SkinHunt.Domain.Models;

namespace SkinHunt.Service.Controllers
{
    [Route("api/signUp")]
    [AllowAnonymous]
    [ApiController]
    public class SignUpController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(IMediator mediator, ILogger<SignUpController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] SignUpModel model)
        {
            try
            {
                var result = await _mediator.Send(new SignUpCommand(model));

                if (result is not null)
                {
                    _logger.LogInformation("User created.");
                    return Ok(result);
                }

                _logger.LogError("Create user failed.");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during sign up");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            } 
        }

        [HttpPost("token")]
        public async Task<int> GetAccessToken(CancellationToken ct)
        {
            return await Task.FromResult(0);
        }

        [HttpGet("token/refresh")]
        public async Task<int> RefreshToken(CancellationToken ct)
        {
            return await Task.FromResult(0);
        }
    }
}
