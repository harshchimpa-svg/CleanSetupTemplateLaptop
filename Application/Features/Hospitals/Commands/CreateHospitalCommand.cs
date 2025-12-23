/*using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Hospitals;
using Domain.Entities.Hospitals.HospitalsTypes;
using MediatR;
using Shared;

namespace Application.Features.Hospitals.Commands;

public class CreateHospitalCommand : IRequest<Result<string>>, ICreateMapFrom<Hospital>
{
    public int Id { get; set; }
    public string HospitalName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PinCode { get; set; }
    public int RegisterNo { get; set; }
    public HospitalType HospitalType { get; set; }
    public string Website { get; set; }
}

internal class CreateHospitalCommandHandler : IRequestHandler<CreateHospitalCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHospitalCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateHospitalCommand request, CancellationToken cancellationToken)
    {
        var country = _mapper.Map<Hospital>(request);

        await _unitOfWork.Repository<Hospital>().AddAsync(country);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Hospital created successfully.");
    }
}


*/