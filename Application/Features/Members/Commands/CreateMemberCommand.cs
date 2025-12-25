using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Memberes;
using MediatR;
using Shared;

namespace Application.Features.Members.Commands;

public class CreateMemberCommand : IRequest<Result<string>>, ICreateMapFrom<Member>
{
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
}
internal class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {

        var Member = _mapper.Map<Member>(request);

        await _unitOfWork.Repository<Member>().AddAsync(Member);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Member created successfully.");
    }
}