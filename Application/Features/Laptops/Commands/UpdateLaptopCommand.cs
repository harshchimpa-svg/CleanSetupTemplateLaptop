using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Laptops;
using Domain.Entities.Rams;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Laptops.Commands;

public class UpdateLaptopCommand : IRequest<Result<Laptop>>
{
    public int Id { get; set; }
    public CreateLaptopCommand CreateCommand { get; set; } = new();

    public UpdateLaptopCommand(int id, CreateLaptopCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateLaptopCommandHandler : IRequestHandler<UpdateLaptopCommand, Result<Laptop>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLaptopCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Laptop>> Handle(UpdateLaptopCommand request, CancellationToken cancellationToken)
    {
        var Laptop = await _unitOfWork.Repository<Laptop>().Entities.Include(x => x.Ram).FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Laptop == null)
        {
            return Result<Laptop>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Laptop);

        await _unitOfWork.Repository<Laptop>().UpdateAsync(Laptop);
        await _unitOfWork.Save(cancellationToken);

        var ram = await _unitOfWork.Repository<Ram>().Entities
            .FirstOrDefaultAsync(x => x.LaptopId == request.Id);

        if (ram != null)
        {
            _mapper.Map(request.CreateCommand, ram);

            await _unitOfWork.Repository<Ram>().UpdateAsync(ram);
        }
        else
        {
            ram = _mapper.Map<Ram>(request.CreateCommand);
            ram.LaptopId = request.Id;

            await _unitOfWork.Repository<Ram>().AddAsync(ram);
        }

        await _unitOfWork.Save(cancellationToken);

        return Result<Laptop>.Success("Update Laptop...");
    }
}
