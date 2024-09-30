using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Commands;
using SkinHunt.Application.Common.Models;
using SkinHunt.Application.Queries;

namespace SkinHunt.Service.Controllers
{
    [Route("api/basket")]
    [Authorize]
    [ApiController]
    public class BasketController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;
        
        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkinsFromBasket([FromQuery] GetSkinsByUserIdFromBasketQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with message: {ex}.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkinsFromBasketCount(string id)
        {
            try
            {
                var result = await _mediator.Send(new GetSkinsFromBasketCountQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with message: {ex}.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSkinToBasket([FromBody] BasketModel model)
        {
            try
            {
                await _mediator.Send(new AddSkinToBasketCommand(model.UserId, model.SkinId));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with message: {ex}.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSkinFromBasket(Guid Id)
        {
            try
            {
                await _mediator.Send(new RemoveSkinFromBasketCommand(Id));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with message: {ex}.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
        }

        [HttpGet("solds/{id}")]
        public async Task<IActionResult> GetSoldsCount(string id)
        {
            try
            {
                var result = await _mediator.Send(new GetSoldsCountQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with message: {ex}.");
                return StatusCode(500, $"Internal server error: {ex.Message}.");
            }
        }
    }
}
