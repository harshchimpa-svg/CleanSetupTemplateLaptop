using Application.Features.Countries.Queries;
using Application.Features.Dashboardes.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Dashboards;

[Route("api/[controller]")]
[ApiController]
public class DashBoardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashBoardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetDashboardQuery());
        return ResponseHelper.GenerateResponse(location);
    }
}
