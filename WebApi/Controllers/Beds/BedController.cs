using Application.Features.Beds.Commands;
using Application.Features.Beds.Queries;
using Application.Features.Chairs.Queries;
using Application.Features.Countries.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Beds
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBed(CreateBedCommands command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBed(int id, CreateBedCommands command)
        {
            var result = await _mediator.Send(new UpdateBedCommands(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBed([FromQuery] GetBedQuerys query)
        {
            var location = await _mediator.Send(query);
            return Ok(location);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBedById(int id)
        {
            var location = await _mediator.Send(new GetByIdBedQuerys(id));
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBed(int id)
        {
            var location = await _mediator.Send(new DeleateBedCommands(id));
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
