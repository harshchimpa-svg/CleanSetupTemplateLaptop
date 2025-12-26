using Application.Features.Members.Queries;
using Application.Features.Rooms.Commands;
using Application.Features.Rooms.Queryes;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Rooms;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateRoom(CreateRoomCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, CreateRoomCommand command)
    {
        var result = await _mediator.Send(new UpdateRoomCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoom([FromQuery] GetMemberQuery query)
    {
        var location = await _mediator.Send(query);
        return Ok(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetRoomById(int id)
    {
        var location = await _mediator.Send(new GetByIdRoomQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var location = await _mediator.Send(new DeleateRoomCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
