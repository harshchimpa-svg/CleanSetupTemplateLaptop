/*using Application.Dto.Hospitals;
using Application.Dto.Locations;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entities.Hospitals;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Hospitals.Commands;

public class UpdateHospitalCommand : IRequest<Result<Hospital>>
{
    public int Id { get; set; }
    public UpdateHospitalDto CreateCommand { get; set; } = new();

    public UpdateHospitalCommand(int id, UpdateHospitalDto createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand, Result<Hospital>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHospitalCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Hospital>> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
    {
        {
            var locationId = await _unitOfWork.Repository<Hospital>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (locationId == null)
            {
                return Result<Hospital>.BadRequest("Sorry location id not found");
            }
            var mapRole = _mapper.Map<Hospital>(request.Id);

            await _unitOfWork.Repository<Hospital>().UpdateAsync(mapRole);
            await _unitOfWork.Save(cancellationToken);
            return Result<Hospital>.Success("Update location...");
        }
    }
}


*/