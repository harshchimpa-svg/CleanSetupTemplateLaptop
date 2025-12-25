using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.BedTypes;
using Domain.Entities.Beds;
using Domain.Entities.Chairs;
using Domain.Entities.Rooms;
using MediatR;
using Shared;

namespace Application.Features.Beds.Commands;

public class CreateBedCommands : IRequest<Result<string>>, ICreateMapFrom<Bed>
{
    public int? RoomId { get; set; }
    public BedType BedType { get; set; }
}
internal class CreateBedCommandsHandler : IRequestHandler<CreateBedCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBedCommandsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateBedCommands request, CancellationToken cancellationToken)
    {
        if (request.RoomId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<Room>().GetByID(request.RoomId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("HouseId does not exist.");
            }
        }

        var House = _mapper.Map<Bed>(request);

        await _unitOfWork.Repository<Bed>().AddAsync(House);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Chair created successfully.");
    }
}