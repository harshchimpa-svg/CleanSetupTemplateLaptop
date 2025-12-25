using Application.Features.HouseMembers.Commands;
using Application.Features.Locations.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.HouseMembers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseMemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HouseMemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLocation(CreateHouseMemberCommand command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
