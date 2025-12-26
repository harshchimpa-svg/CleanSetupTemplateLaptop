using Application.Dto.Memberes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Memberes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Members.Queries;

public class GetMemberQuery : IRequest<PaginatedResult<GetMemberDto>>
{
    public int? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, PaginatedResult<GetMemberDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetMemberDto>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Member>().Entities.AsQueryable();

        if (request.PhoneNumber.HasValue)
        {
            queryable = queryable.Where(x => x.PhoneNumber == request.PhoneNumber);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        int count = await queryable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetMemberDto>>(query);

        return PaginatedResult<GetMemberDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}