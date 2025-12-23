/*using Application.Dto.Hospitals;
using Application.Dto.Locations;
using Application.Features.Countries.Queries;
using Application.Features.Hospitals.Commands;
using Application.Features.Hospitals.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Hospitals;

[Route("api/[controller]")]
[ApiController]
public class HospitalController : ControllerBase
{
    private readonly IMediator _mediator;

    public HospitalController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<ActionResult> CreateCountry(CreateHospitalCommand command)
    {
        var data = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenu(int id, [FromForm] UpdateHospitalDto Command)
    {
        var command = new CreateHospitalCommand(id, Command);
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var data = await _mediator.Send(new GetHospitaQuery());
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCountryById(int id)
    {
        var data = await _mediator.Send(new GetByIdHospitalQuery(id));
        return ResponseHelper.GenerateResponse(data);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(int id)
    {
        var data = await _mediator.Send(new DeleteHospitalCommand(id));
        return ResponseHelper.GenerateResponse(data);
    }
}
*/