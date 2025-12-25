using Application.Common.Mappings.Commons;
using Application.Dto.HouseMembers;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.HouseMembers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.HouseMembers.Commands;

public class CreateHouseMemberCommand : IRequest<Result<GetHouseMemberDto>>, ICreateMapFrom<HouseMember>
{
    public int HouseId { get; set; }
    public int MemberId { get; set; }
}
internal class CreateHouseMemberCommandHandler : IRequestHandler<CreateHouseMemberCommand, Result<GetHouseMemberDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHouseMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetHouseMemberDto>> Handle(CreateHouseMemberCommand request, CancellationToken cancellationToken)
    {
        var subject = _mapper.Map<HouseMember>(request);

        await _unitOfWork.Repository<HouseMember>().AddAsync(subject);
        await _unitOfWork.Save(cancellationToken);

        var result = await _unitOfWork.Repository<HouseMember>().Entities
            .Include(x => x.House)
            .Include(x => x.Member)
            .FirstOrDefaultAsync(x=>x.Id==subject.Id);

        var mapData=_mapper.Map<GetHouseMemberDto>(result);

        return Result<GetHouseMemberDto>.Success(mapData, "HouseMember created successfully.");
    }
}