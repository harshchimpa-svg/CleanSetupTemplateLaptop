using Application.Dto.Laptops;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Laptops;
using MediatR;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Laptops.Queries;

public class GetLaptopQuery : IRequest<Result<List<GetLaptopDto>>>;

internal class GetLaptopQueryHandler : IRequestHandler<GetLaptopQuery, Result<List<GetLaptopDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLaptopQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetLaptopDto>>> Handle( GetLaptopQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Laptop>().Entities
            .Include(x => x.Ram)
            .ToListAsync();

        var map = _mapper.Map<List<GetLaptopDto>>(locations);

        return Result<List<GetLaptopDto>>.Success(map, "Location list");
    }
}
