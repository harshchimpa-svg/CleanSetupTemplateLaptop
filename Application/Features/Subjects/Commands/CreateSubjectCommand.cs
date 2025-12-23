using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Subjects;
using MediatR;
using Shared;

namespace Application.Features.Subjects.Commands;

public class CreateSubjectCommand : IRequest<Result<string>>, ICreateMapFrom<Subject>
{
    public string Name { get; set; }
    public int ClassId { get; set; }

}
internal class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = _mapper.Map<Subject>(request);

        await _unitOfWork.Repository<Subject>().AddAsync(subject);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Subject created successfully.");
    }
}