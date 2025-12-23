/*using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Employees;
using Domain.Entities.Hospitals;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Hospitals.Commands;

public class DeleteHospitalCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteHospitalCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;

    public DeleteHospitalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Hospital>().Entities
       .AnyAsync(x => x.Id == request.Id && !x.IsDeleted);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Hospital not found.");
        }

        await _unitOfWork.Repository<Hospital>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Hospital deleted successfully.");
    }
}*/