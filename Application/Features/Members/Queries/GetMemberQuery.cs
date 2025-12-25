using Application.Dto.Locations;
using Application.Dto.Memberes;
using Application.Features.Locations.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Memberes;
using MediatR;
using Shared;

namespace Application.Features.Members.Queries;

public class GetMemberQuery : IRequest<Result<List<GetMemberDto>>>
{
}
internal class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, Result<List<GetMemberDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetMemberDto>>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Member>().GetAll();

        var map = _mapper.Map<List<GetMemberDto>>(locations);

        return Result<List<GetMemberDto>>.Success(map, "Location list");
    }
}