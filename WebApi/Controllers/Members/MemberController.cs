using Application.Features.Countries.Queries;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries;
using Application.Features.Members.Commands;
using Application.Features.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLocation(CreateMemberCommand command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, CreateMemberCommand command)
        {
            var result = await _mediator.Send(new UpdateMemberCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLocation()
        {
            var location = await _mediator.Send(new GetMemberQuery());
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLocationById(int id)
        {
            var location = await _mediator.Send(new GetByIdMemberQuery(id));
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _mediator.Send(new DeleateMemberCommand(id));
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
