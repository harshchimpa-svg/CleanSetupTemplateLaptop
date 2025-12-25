using Application.Dto.Chairs;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Chairs;
using MediatR;
using Shared;

namespace Application.Features.Chairs.Queries;

public class GetByIdChairQuery : IRequest<Result<GetChairDto>>
{
    public int Id { get; set; }

    public GetByIdChairQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdChairQueryHandler : IRequestHandler<GetByIdChairQuery, Result<GetChairDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdChairQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetChairDto>> Handle(GetByIdChairQuery request, CancellationToken cancellationToken)
    {
        var Chair = await _unitOfWork.Repository<Chair>().GetByID(request.Id);

        if (Chair == null)
        {
            return Result<GetChairDto>.BadRequest("Chair not found.");
        }

        var mapData = _mapper.Map<GetChairDto>(Chair);

        return Result<GetChairDto>.Success(mapData, "Chair");
    }
}