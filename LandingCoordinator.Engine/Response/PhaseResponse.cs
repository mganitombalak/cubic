using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine.Response
{
    public class PhaseResponse : IPhaseResponse
    {
        public IVectorElement<int> RequestedPosition { get; set; }
        public bool IsPhaseFailed { get; init; }
        public string Message { get; init; }
        public Status OperaStatus { get; init; }
    }
}