using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Subjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects.Commands;

public class UpdateSubjectCommand : IRequest<Result<Subject>>
{

    public int Id { get; set; }
    public CreateSubjectCommand CreateCommand { get; set; } = new();

    public UpdateSubjectCommand(int id, CreateSubjectCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Result<Subject>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Subject>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {

        var subject = await _unitOfWork.Repository<Subject>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (subject == null)
        {
            return Result<Subject>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, subject);

        await _unitOfWork.Repository<Subject>().UpdateAsync(subject);
        await _unitOfWork.Save(cancellationToken);

        return Result<Subject>.Success("Update subject...");
    }
}