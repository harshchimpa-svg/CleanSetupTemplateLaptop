/*using Application.Dto.Hospitals;
using Application.Dto.Locations;
using Application.Features.Countries.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Hospitals;
using MediatR;
using Shared;

namespace Application.Features.Hospitals.Queries;

public class GetByIdHospitalQuery : IRequest<Result<GetLocationDto>>
{
    public int Id { get; set; }

    public GetByIdHospitalQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdHospitalQueryHandler : IRequestHandler<GetByIdHospitalQuery, Result<GetHospitalDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHospitalQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetHospitalDto>> Handle(GetByIdHospitalQuery request, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.Repository<Hospital>().GetByID(request.Id);

        if (country == null)
        {
            return Result<GetHospitalDto>.BadRequest("Country not found.");
        }

        var mapData = _mapper.Map<GetHospitalDto>(country);

        return Result<GetHospitalDto>.Success(mapData, "Country");
    }
}

*/