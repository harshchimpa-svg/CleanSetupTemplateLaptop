using Application.Dto.Beds;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Beds;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Beds.Queries;

public class GetBedQuerys : IRequest<PaginatedResult<GetBedDto>>
{
    public int? RoomId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetBedQuerysHandler : IRequestHandler<GetBedQuerys,PaginatedResult<GetBedDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBedQuerysHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetBedDto>> Handle(GetBedQuerys request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Bed>().Entities.Include(s => s.Room)
          .AsQueryable();

        if (request.RoomId.HasValue)
        {
            queryable = queryable.Where(x => x.RoomId == request.RoomId);
        }
        int count = await queryable.CountAsync();


        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetBedDto>>(query);

        return PaginatedResult < GetBedDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}