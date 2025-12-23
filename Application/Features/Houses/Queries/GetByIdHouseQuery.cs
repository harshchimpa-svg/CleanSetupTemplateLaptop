using Application.Dto.Houses;
using Application.Dto.Locations;
using Application.Features.Countries.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Houses;
using MediatR;
using Shared;

namespace Application.Features.Houses.Queries;

public class GetByIdHouseQuery : IRequest<Result<GetHouseDto>>
{
    public int Id { get; set; }

    public GetByIdHouseQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdHouseQueryHandler : IRequestHandler<GetByIdHouseQuery, Result<GetHouseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHouseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetHouseDto>> Handle(GetByIdHouseQuery request, CancellationToken cancellationToken)
    {
        var House = await _unitOfWork.Repository<House>().GetByID(request.Id);

        if (House == null)
        {
            return Result<GetHouseDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetHouseDto>(House);

        return Result<GetHouseDto>.Success(mapData, "Location");
    }
}