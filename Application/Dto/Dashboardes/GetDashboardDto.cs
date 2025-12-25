namespace Application.Dto.Dashboardes;

public class GetDashboardDto
{
    public int HouseCount { get; set; }
    public int RoomCount { get; set; }
    public int ChairCount { get; set; }
    public int BedCount { get; set; }
    public int MemberCount { get; set; }


    public double AvgRoomsPerHouse { get; set; }
    public double AvgChairsPerRoom { get; set; }
    public double AvgBedsPerRoom { get; set; }
    public double AvgMembersPerHouse { get; set; }
}

