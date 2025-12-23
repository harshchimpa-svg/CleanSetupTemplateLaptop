using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Laptops;
using Domain.Entities.Rams;
using MediatR;
using Shared;

namespace Application.Features.Laptops.Commands;

public class CreateLaptopCommand: IRequest<Result<string>>, ICreateMapFrom<Ram>,ICreateMapFrom<Laptop>
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public decimal Storage { get; set; }
}

internal class CreateLaptopCommandHandler: IRequestHandler<CreateLaptopCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLaptopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateLaptopCommand request,CancellationToken cancellationToken)
    {
        var laptop = _mapper.Map<Laptop>(request);
        var ram = _mapper.Map<Ram>(request);


        await _unitOfWork.Repository<Laptop>().AddAsync(laptop);
        await _unitOfWork.Save(cancellationToken);

        ram.LaptopId=laptop.Id;
        await _unitOfWork.Repository<Ram>().AddAsync(ram);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Laptop created successfully.");
    }
}
