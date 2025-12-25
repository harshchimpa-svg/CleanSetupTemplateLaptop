using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Memberes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Members.Commands;

public class DeleateMemberCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateMemberCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateMemberCommandHandler : IRequestHandler<DeleateMemberCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateMemberCommand request, CancellationToken cancellationToken)
    {
        var MemberExists = await _unitOfWork.Repository<Member>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!MemberExists)
        {
            return Result<bool>.BadRequest("Member not found.");
        }

        await _unitOfWork.Repository<Member>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Member deleted successfully.");
    }
}