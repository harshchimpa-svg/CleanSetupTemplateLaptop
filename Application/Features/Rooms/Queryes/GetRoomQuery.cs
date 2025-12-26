using Application.Dto.Rooms;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Linq;

namespace Application.Features.Rooms.Queryes;

public class GetRoomQuery : IRequest<PaginatedResult<GetRoomDto>>
{
    public string? Name { get; set; }
    public int? Size { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, PaginatedResult<GetRoomDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetRoomDto>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Room>().Entities.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        if (request.Size.HasValue)
        {
            queryable = queryable.Where(x => x.Size == request.Size);
        }

        int count = await queryable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                  .Skip((request.PageNumber - 1) * request.PageSize)
                  .Take(request.PageSize);
        } 
        var locations = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetRoomDto>>(locations);

        return PaginatedResult<GetRoomDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}