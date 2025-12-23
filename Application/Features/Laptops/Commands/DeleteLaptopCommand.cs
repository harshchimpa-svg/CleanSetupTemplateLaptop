using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Laptops;
using Domain.Entities.Rams;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Laptops.Commands;

public class DeleteLaptopCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteLaptopCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteLaptopCommandHandler : IRequestHandler<DeleteLaptopCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLaptopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteLaptopCommand request, CancellationToken cancellationToken)
    {
        var laptop = await _unitOfWork.Repository<Laptop>().Entities.Include(x => x.Ram).FirstOrDefaultAsync(x => x.Id == request.Id);

        if (laptop == null)
        {
            return Result<bool>.BadRequest("Laptop not found.");
        }

        await _unitOfWork.Repository<Laptop>().DeleteAsync(laptop);
        await _unitOfWork.Save(cancellationToken);

        var ram = await _unitOfWork.Repository<Ram>().Entities
            .FirstOrDefaultAsync(x => x.LaptopId == request.Id);

        if (ram != null)
        {
            await _unitOfWork.Repository<Ram>().DeleteAsync(ram);
            await _unitOfWork.Save(cancellationToken);
        }

        return Result<bool>.Success(true, "Laptop deleted successfully.");
    }
}