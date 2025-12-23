using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Locations.LocationTypes;
using MediatR;
using Shared;
using Location = Domain.Locations.Location;

namespace Application.Features.Locations.Commands;

public class CreateLocationCommand : IRequest<Result<string>>, ICreateMapFrom<Location>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? ParentId { get; set; }
    public LocationType? LocationType { get; set; }
}

internal class CreateLocationCommandHandler: IRequestHandler<CreateLocationCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateLocationCommand request,CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Location>().GetByID(request.ParentId.Value);

            if (parentExists == null)
            {
                return Result<string>.BadRequest("Parent Id is not exist.");
            }
        }

        var location = _mapper.Map<Location>(request);

        await _unitOfWork.Repository<Location>().AddAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Location created successfully.");
    }
}
