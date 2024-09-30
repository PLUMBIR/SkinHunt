using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinHunt.Application.Queries;

namespace SkinHunt.Service.Controllers
{
    [Route("api/transactions")]
    [Authorize]
    [ApiController]
    public class TransactionsController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public TransactionsController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactions(string id)
        {
            try
            {
                var result = await _mediator.Send(new GetTransactionsQuery() { UserId = id});

                _logger.LogInformation("Transactions received.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occured during getting transactions.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
