/*using Application.Dto.Hospitals;
using Application.Dto.Locations;
using Application.Features.Locations.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Hospitals;
using MediatR;
using Shared;

namespace Application.Features.Hospitals.Queries;

public class GetHospitaQuery : IRequest<Result<List<GetHospitalDto>>>
{
}
internal class GetHospitaQueryHandler : IRequestHandler<GetHospitaQuery, Result<List<GetHospitalDto>>>
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _UnitOfWork;

    public GetHospitaQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _UnitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetHospitalDto>>> Handle(GetHospitaQuery request, CancellationToken cancellationToken)
    {
        var getLocationType = await _UnitOfWork.Repository<Hospital>().GetAll();

        var mapLocationType = _mapper.Map<List<GetHospitalDto>>(getLocationType);

        return Result<List<GetHospitalDto>>.Success(mapLocationType, "MenuType list");
    }
}




*/