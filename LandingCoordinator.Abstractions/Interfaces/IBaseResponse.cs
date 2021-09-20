using LandingCoordinator.Abstractions.Enums;

namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface IBaseResponse
    {
        Status Status { get; set; }
    }
}