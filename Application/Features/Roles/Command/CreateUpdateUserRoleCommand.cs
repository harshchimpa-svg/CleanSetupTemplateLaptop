using Application.Interfaces.Repositories.Users.UserRoles;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Command;

public class CreateUpdateUserRoleCommand : IRequest<Result<string>>
{
    public CreateUpdateUserRoleCommand(string userId, List<string> roles)
    {
        UserId = userId;
        Roles = roles;
    }

    public string UserId { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
internal class CreateUserRoleCommandHandler : IRequestHandler<CreateUpdateUserRoleCommand, Result<string>>
{
    private readonly IUserRoleRepository _repository;
    private readonly UserManager<User> _userManager;

    public CreateUserRoleCommandHandler(IUserRoleRepository repository, UserManager<User> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(CreateUpdateUserRoleCommand request, CancellationToken cancellationToken)
    {

        var checkUser = await _userManager.Users.AnyAsync(x => x.Id == request.UserId);

        if (!checkUser)
        {
            return Result<string>.BadRequest("User doesn't exists");
        }

        await _repository.CreateUpdateUserRole(request.UserId, request.Roles);

        return Result<string>.Success("Roles assigned to user Successfully");
    }
}
