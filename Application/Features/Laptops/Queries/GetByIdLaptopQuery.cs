using Application.Dto.Laptops;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Laptops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Laptops.Queries;

public class GetByIdLaptopQuery : IRequest<Result<GetLaptopDto>>
{
    public int Id { get; set; }

    public GetByIdLaptopQuery(int id)
    {
        Id = id;
    }
}

internal class GetByIdLaptopQueryHandler : IRequestHandler<GetByIdLaptopQuery, Result<GetLaptopDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdLaptopQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetLaptopDto>> Handle(GetByIdLaptopQuery request, CancellationToken cancellationToken)
    {
        var Laptop = await _unitOfWork.Repository<Laptop>().Entities
                       .Include(x => x.Ram)
                       .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Laptop == null)
        {
            return Result<GetLaptopDto>.BadRequest("Laptop not found.");
        }

        var mapData = _mapper.Map<GetLaptopDto>(Laptop);

        return Result<GetLaptopDto>.Success(mapData, "Laptop");
    }
}
 