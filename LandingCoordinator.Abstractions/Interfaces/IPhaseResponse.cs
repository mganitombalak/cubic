using LandingCoordinator.Abstractions.Enums;

namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface IPhaseResponse
    {
        IVectorElement<int> RequestedPosition { get; set; }
        bool IsPhaseFailed { get; init; }
        string Message { get; init; }
        Status OperaStatus { get; init; }
    }
}