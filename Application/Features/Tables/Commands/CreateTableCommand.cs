using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Commands;

public class CreateTableCommand : IRequest<Result<string>>, ICreateMapFrom<Table>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public int? ParentId { get; set; }
}
internal class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTableCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Table>().GetByID(request.ParentId.Value);

            if (parentExists == null)
            {
                return Result<string>.BadRequest("Parent Id is not exist.");
            }
        }

        var Table = _mapper.Map<Table>(request);

        await _unitOfWork.Repository<Table>().AddAsync(Table);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Table created successfully.");
    }
}

