using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Commands;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;
using DbContext = SkinHunt.Application.DbContext;

namespace SkinHunt.Service.Controllers
{
    [Route("api/skin")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class SkinController : AppControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SkinController> _logger;
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;

        public SkinController(
            IMediator mediator,
            ILogger<SkinController> logger,
            IMapper mapper, DbContext dbContext)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddSkin([FromBody] SkinModel model)
        {
            try
            {
                await _mediator.Send(new AddItemTypeCommand(model.Type));

                var skinEntity = _mapper.Map<SkinEntity>(model);

                var skinType = await _dbContext.SkinTypes
                    .FirstAsync(o => o.Category == model.Type.Category && o.Subcategory == model.Type.Subcategory);
                
                skinEntity.Type = skinType;

                var result = await _mediator.Send(new AddSkinCommand(skinEntity));

                _logger.LogInformation($"Added skin to db. Name: {result.Name}, Type: category - {result.Type.Category}, " +
                    $"subcategory - {result.Type.Subcategory}, Float: {result.Float}");

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError("Unexpected error occured during add skin");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
