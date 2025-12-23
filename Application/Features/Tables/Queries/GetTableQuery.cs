using Application.Dto.Locations;
using Application.Dto.Tables;
using Application.Features.Locations.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Queries;

public class GetTableQuery : IRequest<Result<List<GetTableDto>>>
{
}
internal class GetTableQueryHandler : IRequestHandler<GetTableQuery, Result<List<GetTableDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTableQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetTableDto>>> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Table>().GetAll();

        var map = _mapper.Map<List<GetTableDto>>(locations);

        return Result<List<GetTableDto>>.Success(map, "Location list");
    }
}
