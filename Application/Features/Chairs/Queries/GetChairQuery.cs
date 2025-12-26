using Application.Dto.Chairs;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Chairs.Queries;

public class GetChairQuery : IRequest <PaginatedResult<GetChairDto>>
{
    public int? HouseId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}   
internal class GetChairQueryHandler : IRequestHandler<GetChairQuery, PaginatedResult<GetChairDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetChairQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetChairDto>> Handle(GetChairQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Chair>().Entities.AsQueryable();


        if (request.HouseId.HasValue)
        {
            queryable = queryable.Where(x => x.HouseId == request.HouseId);
        }

        int count = await queryable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetChairDto>>(query);

        return PaginatedResult<GetChairDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}