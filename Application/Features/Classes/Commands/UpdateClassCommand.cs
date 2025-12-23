using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Classes.Commands;

public class UpdateClassCommand : IRequest<Result<Class>>
{

    public int Id { get; set; }
    public CreateClassCommand CreateCommand { get; set; } = new();

    public UpdateClassCommand(int id, CreateClassCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, Result<Class>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClassCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Class>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {

        var Class = await _unitOfWork.Repository<Class>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Class == null)
        {
            return Result<Class>.BadRequest("Class id not found");
        }

        _mapper.Map(request.CreateCommand, Class);

        await _unitOfWork.Repository<Class>().UpdateAsync(Class);
        await _unitOfWork.Save(cancellationToken);

        return Result<Class>.Success("Update Class...");
    }
}