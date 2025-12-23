using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Tables.Commands;

public class UpdateTableCommand : IRequest<Result<Table>>
{

    public int Id { get; set; }
    public CreateTableCommand CreateCommand { get; set; } 

    public UpdateTableCommand(int id, CreateTableCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, Result<Table>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTableCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Table>> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.ParentId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Table>().GetByID(request.CreateCommand.ParentId.Value);

            if (parent == null)
            {
                return Result<Table>.BadRequest("Parent Id is not exist.");
            }
        }

        var table = await _unitOfWork.Repository<Table>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (table == null)
        {
            return Result<Table>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, table);

        await _unitOfWork.Repository<Table>().UpdateAsync(table);
        await _unitOfWork.Save(cancellationToken);

        return Result<Table>.Success("Update table...");
    }
}