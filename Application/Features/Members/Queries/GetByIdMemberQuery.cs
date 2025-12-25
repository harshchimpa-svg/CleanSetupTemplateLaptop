using Application.Dto.Locations;
using Application.Dto.Memberes;
using Application.Features.Countries.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Memberes;
using MediatR;
using Shared;

namespace Application.Features.Members.Queries;

public class GetByIdMemberQuery : IRequest<Result<GetMemberDto>>
{
    public int Id { get; set; }

    public GetByIdMemberQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdMemberQueryHandler : IRequestHandler<GetByIdMemberQuery, Result<GetMemberDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdMemberQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetMemberDto>> Handle(GetByIdMemberQuery request, CancellationToken cancellationToken)
    {
        var Member = await _unitOfWork.Repository<Member>().GetByID(request.Id);

        if (Member == null)
        {
            return Result<GetMemberDto>.BadRequest("Member not found.");
        }

        var mapData = _mapper.Map<GetMemberDto>(Member);

        return Result<GetMemberDto>.Success(mapData, "Member");
    }
}