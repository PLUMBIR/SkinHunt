using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Common.Models;
using SkinHunt.Application.Queries;

namespace SkinHunt.Service.Controllers
{
    [Route("api/skins")]
    [AllowAnonymous]
    [ApiController]
    public class GetAllSkinsController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetAllSkinsController> _logger;

        public GetAllSkinsController(IMediator mediator, ILogger<GetAllSkinsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinsAsync([FromQuery] GetSkinsQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get all skins from db.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
            
        }
    }
}
