using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.ChairLegTypes;
using Domain.Entities.Chairs;
using Domain.Entities.Houses;
using MediatR;
using Shared;

namespace Application.Features.Chairs.Commands;

public class CreateChairCommands: IRequest<Result<string>>, ICreateMapFrom<Chair>
{
    public string Name { get; set; }
    public int? HouseId { get; set; }
    public ChairLegType ChairLegType { get; set; }
}

internal class CreateChairCommandsHandler: IRequestHandler<CreateChairCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateChairCommandsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateChairCommands request,CancellationToken cancellationToken)
    {
        if (request.HouseId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<House>().GetByID(request.HouseId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("HouseId does not exist.");
            }
        }

        var House = _mapper.Map<Chair>(request);

        await _unitOfWork.Repository<Chair>().AddAsync(House);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Chair created successfully.");
    }
}
