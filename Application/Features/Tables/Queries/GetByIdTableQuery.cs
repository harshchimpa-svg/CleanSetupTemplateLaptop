using Application.Dto.Locations;
using Application.Dto.Tables;
using Application.Features.Countries.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Queries;

public class GetByIdTableQuery : IRequest<Result<GetTableDto>>
{
    public int Id { get; set; }

    public GetByIdTableQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdTableQueryHandler : IRequestHandler<GetByIdTableQuery, Result<GetTableDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdTableQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetTableDto>> Handle(GetByIdTableQuery request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Table>().GetByID(request.Id);

        if (table == null)
        {
            return Result<GetTableDto>.BadRequest("table not found.");
        }

        var mapData = _mapper.Map<GetTableDto>(table);

        return Result<GetTableDto>.Success(mapData, "table");
    }
}
