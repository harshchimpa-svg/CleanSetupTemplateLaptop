using Application.Dto.Classes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Classes.Queries;

public class GetClassQuery : IRequest<Result<List<GetClassDto>>>
{
}
internal class GetClassQueryHandler : IRequestHandler<GetClassQuery, Result<List<GetClassDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClassQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetClassDto>>> Handle(GetClassQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Class>().Entities.Include(s => s.Subject).ThenInclude(x=>x.Lesson)
            .AsQueryable(); 
                   
        var Class = await query.ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetClassDto>>(Class);

        return Result<List<GetClassDto>>.Success(map, "Class list");
    }
}