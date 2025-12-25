using Application.Dto.Beds;
using Application.Dto.Chairs;
using Application.Features.Chairs.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Beds;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Beds.Queries;

public class GetBedQuerys : IRequest<Result<List<GetBedDto>>>
{
}
internal class GetBedQuerysHandler : IRequestHandler<GetBedQuerys, Result<List<GetBedDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBedQuerysHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetBedDto>>> Handle(GetBedQuerys request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Bed>().Entities.Include(s => s.Room)
          .AsQueryable();

        var Chair = await query.ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetBedDto>>(Chair);

        return Result<List<GetBedDto>>.Success(map, "Chair list");
    }
}