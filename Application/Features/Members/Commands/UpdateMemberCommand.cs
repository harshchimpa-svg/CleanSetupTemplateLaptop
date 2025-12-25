using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Memberes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Members.Commands;

public class UpdateMemberCommand : IRequest<Result<Member>>
{
     
    public int Id { get; set; }
    public CreateMemberCommand CreateCommand { get; set; } = new();

    public UpdateMemberCommand(int id, CreateMemberCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Result<Member>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Member>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {

        var Member = await _unitOfWork.Repository<Member>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Member == null)
        {
            return Result<Member>.BadRequest("Member id not found");
        }

        _mapper.Map(request.CreateCommand, Member);

        await _unitOfWork.Repository<Member>().UpdateAsync(Member);
        await _unitOfWork.Save(cancellationToken);

        return Result<Member>.Success("Update Member...");
    }
}