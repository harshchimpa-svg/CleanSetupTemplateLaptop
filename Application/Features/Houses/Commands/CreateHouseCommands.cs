using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Houses;
using MediatR;
using Shared;

namespace Application.Features.Houses.Commands;

public class CreateHouseCommands : IRequest<Result<string>>, ICreateMapFrom<House>
{
    public string Name { get; set; }

}
internal class CreateHouseCommandsHandler : IRequestHandler<CreateHouseCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHouseCommandsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateHouseCommands request, CancellationToken cancellationToken)
    {

        var House = _mapper.Map<House>(request);

        await _unitOfWork.Repository<House>().AddAsync(House);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("House created successfully.");
    }
}