using Application.Dto.Chairs;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Chairs.Queries;

public class GetChairQuery : IRequest<Result<List<GetChairDto>>>
{
} 
internal class GetChairQueryHandler : IRequestHandler<GetChairQuery, Result<List<GetChairDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetChairQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetChairDto>>> Handle(GetChairQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Chair>().Entities.Include(s => s.House)
          .AsQueryable();

        var Chair = await query.ToListAsync(cancellationToken);

       var map = _mapper.Map<List<GetChairDto>>(Chair);

        return Result<List<GetChairDto>>.Success(map, "Chair list");
    }
}