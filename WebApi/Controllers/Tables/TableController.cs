using Application.Features.Countries.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using Application.Features.Tables.Commands;
using Application.Features.Tables.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Tables;

[Route("api/[controller]")]
[ApiController]
public class TableController : ControllerBase
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult> CreateLocation(CreateTableCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, CreateTableCommand command)
    {
        var result = await _mediator.Send(new UpdateTableCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetTableQuery());
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetLocationById(int id)
    {
        var location = await _mediator.Send(new GetByIdTableQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _mediator.Send(new DeletTableCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
