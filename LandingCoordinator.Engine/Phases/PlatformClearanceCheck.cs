using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine.Response;
using Microsoft.Extensions.Logging;

namespace LandingCoordinator.Engine.Phases
{
    public class PlatformClearanceCheck : ILandingPhase
    {
        private readonly LandingContext _context;

        public PlatformClearanceCheck(LandingContext context)
        {
            _context = context;
        }

        public IPhaseResponse Process(ILogger logger)
        {
            logger.LogInformation("Platform Clearance Check");
            var result = LandingContext.LandingArea.PlatformPosition.i <= _context.RequestedPosition.i &&
                         _context.RequestedPosition.i > LandingContext.LandingArea.PlatformBoundaries.i ||
                         LandingContext.LandingArea.PlatformPosition.j <= _context.RequestedPosition.j &&
                         _context.RequestedPosition.j > LandingContext.LandingArea.PlatformBoundaries.j ||
                         _context.RequestedPosition.i <= 0 ||
                         _context.RequestedPosition.j <= 0;
            return new PhaseResponse
            {
                IsPhaseFailed = result, Message = "OutOfPlatform", OperaStatus = Status.OutOfPlatform,
                RequestedPosition = _context.RequestedPosition
            };
        }
    }
}