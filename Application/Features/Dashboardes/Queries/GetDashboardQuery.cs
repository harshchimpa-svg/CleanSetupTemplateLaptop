using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Beds;
using Domain.Entities.Chairs;
using Domain.Entities.Houses;
using Domain.Entities.Memberes;
using Domain.Entities.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dashboardes.Queries;

public class GetDashboardQuery : IRequest<Result<GetDashboardDto>>
{
}
internal class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Result<GetDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDashboardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var houseCount = await _unitOfWork.Repository<House>().Entities.CountAsync();
        var roomCount = await _unitOfWork.Repository<Room>().Entities.CountAsync();
        var chairCount = await _unitOfWork.Repository<Chair>().Entities.CountAsync();
        var bedCount = await _unitOfWork.Repository<Bed>().Entities.CountAsync();
        var memberCount = await _unitOfWork.Repository<Member>().Entities.CountAsync();

        var dto = new GetDashboardDto
        {
            HouseCount = houseCount,
            RoomCount = roomCount,
            ChairCount = chairCount,
            BedCount = bedCount, 
            MemberCount = memberCount,

            AvgRoomsPerHouse = houseCount == 0 ? 0 : roomCount / houseCount,
            AvgChairsPerRoom = houseCount == 0 ? 0 :   chairCount/roomCount,
            AvgBedsPerRoom = houseCount == 0 ? 0 :   bedCount/roomCount,
            AvgMembersPerHouse = houseCount == 0 ? 0 : memberCount / houseCount

        };

        return Result<GetDashboardDto>.Success(dto, "Dashboard count");
    }

}