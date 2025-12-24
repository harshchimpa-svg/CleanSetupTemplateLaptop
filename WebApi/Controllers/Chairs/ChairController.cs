using Application.Features.Chairs.Commands;
using Application.Features.Chairs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Chairs;

[Route("api/[controller]")]
[ApiController]
public class ChairController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChairController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateChair(CreateChairCommands command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChair(int id, CreateChairCommands command)
    {
        var result = await _mediator.Send(new UpdateChairCommands(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetChair()
    {
        var Chair = await _mediator.Send(new GetChairQuery());
        return ResponseHelper.GenerateResponse(Chair);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetChairById(int id)
    {
        var Chair = await _mediator.Send(new GetByIdChairQuery(id));
        return ResponseHelper.GenerateResponse(Chair);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChair(int id)
    {
        var Chair = await _mediator.Send(new DeleateChairCommands(id));
        return ResponseHelper.GenerateResponse(Chair);
    }
}
