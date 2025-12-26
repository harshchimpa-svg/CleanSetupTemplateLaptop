using Application.Features.Houses.Commands;
using Application.Features.Houses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Houses;

[Route("api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public HousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateHouse(CreateHouseCommands command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHouse(int id, CreateHouseCommands command)
    {
        var result = await _mediator.Send(new UpdateHouseCommands(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetHouse([FromQuery] GetHouseQuery query)
    {
        var location = await _mediator.Send(query);
        return Ok(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetHouseById(int id)
    {
        var location = await _mediator.Send(new GetByIdHouseQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHouse(int id)
    {
        var location = await _mediator.Send(new DeleteHouseCommands(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
