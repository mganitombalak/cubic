using Microsoft.Extensions.Logging;

namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface ILandingPhase
    {
        IPhaseResponse Process(ILogger logger);
    }
}