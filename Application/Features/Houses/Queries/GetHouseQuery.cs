using Application.Dto.Houses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Houses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Linq;

namespace Application.Features.Houses.Queries;

public class GetHouseQuery : IRequest<PaginatedResult<GetHouseDto>>
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } 

}
internal class GetHouseQueryHandler : IRequestHandler<GetHouseQuery,PaginatedResult<GetHouseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHouseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetHouseDto>> Handle(GetHouseQuery request, CancellationToken cancellationToken)
    {
        var queryable =  _unitOfWork.Repository<House>().Entities.AsQueryable();

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
        var locations = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetHouseDto>>(locations);

        return PaginatedResult<GetHouseDto>.Create(map, count,request.PageNumber,request.PageSize);
    }
}