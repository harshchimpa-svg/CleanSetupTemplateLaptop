using Application.Features.Countries.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using Application.Features.Subjects.Commands;
using Application.Features.Subjects.Queryes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Subjects;

[Route("api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLocation(CreateSubjectCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, CreateSubjectCommand command)
    {
        var result = await _mediator.Send(new UpdateSubjectCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetSubjectQuery());
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetLocationById(int id)
    {
        var location = await _mediator.Send(new GetByIdSubjectQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _mediator.Send(new DeleateSubjectCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
