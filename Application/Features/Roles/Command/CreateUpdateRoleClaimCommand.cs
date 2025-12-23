using Application.Interfaces.Repositories.Claims;
using Domain.Entities.ApplicationRoles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Command;

public class CreateUpdateRoleClaimCommand : IRequest<Result<int>>
{
    public CreateUpdateRoleClaimCommand(Guid roleId, string[] menuId)
    {
        RoleId = roleId;
        MenuId = menuId;
    }

    public Guid RoleId { get; set; }
    public string[] MenuId { get; set; } = [];
}

internal class CreateRoleClaimCommandHandler : IRequestHandler<CreateUpdateRoleClaimCommand, Result<int>>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IClaimRepository _roleClaimRepository;

    public CreateRoleClaimCommandHandler(RoleManager<Role> roleManager, IClaimRepository roleClaimRepository)
    {
        _roleManager = roleManager;
        _roleClaimRepository = roleClaimRepository;
    }

    public async Task<Result<int>> Handle(CreateUpdateRoleClaimCommand request, CancellationToken cancellationToken)
    {
        var checkRole = await _roleManager.Roles.AnyAsync(x => x.Id == request.RoleId.ToString());

        if (!checkRole)
        {
            return Result<int>.BadRequest("Role id not found");
        }

        await _roleClaimRepository.Create(request);

        return Result<int>.Success("Role menu claims created successfully");

    }
}
