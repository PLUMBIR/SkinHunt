using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Commands;
using SkinHunt.Application.Queries;
using SkinHunt.Domain.Models;

namespace SkinHunt.Service.Controllers
{
    [Route("api/signIn")]
    [AllowAnonymous]
    [ApiController]
    public class SignInController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SignInController> _logger;

        public SignInController(IMediator mediator, ILogger<SignInController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignInModel model)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByEmailQuery(model.Email));

                if (user is not null)
                {
                    var result = await _mediator.Send(new SignInCommand(user.UserName, model.Password));

                    if (result is not null)
                    {
                        _logger.LogInformation("Log in successeded.");
                        return Ok(result);
                    }

                    _logger.LogError("Log in failed: password is incorrect.");
                    return NoContent();
                }

                _logger.LogError("Log in failed: user not found.");
                return NoContent();          
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during log in.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }  
        }
    }
}
