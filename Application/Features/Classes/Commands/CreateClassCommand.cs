using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Classes;
using MediatR;
using Shared;

namespace Application.Features.Classes.Commands;

public class CreateClassCommand : IRequest<Result<string>>, ICreateMapFrom<Class>
{
    public string Name { get; set; }

}
internal class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<string>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {

        var Class = _mapper.Map<Class>(request);

        await _unitOfWork.Repository<Class>().AddAsync(Class);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Class created successfully.");
    }
}



