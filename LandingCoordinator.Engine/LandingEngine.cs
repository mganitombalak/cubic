using System.Collections.Generic;
using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine.Phases;
using LandingCoordinator.Engine.Response;
using Microsoft.Extensions.Logging;

namespace LandingCoordinator.Engine
{
    public class LandingEngine
    {
        private static IEnumerable<ILandingPhase> Phases;
        private readonly LandingContext _landingContext;

        public LandingEngine(ILandingAreaParameter landingAreaParamaters)
        {
            _landingContext = new LandingContext(landingAreaParamaters);
            Phases = new List<ILandingPhase>
            {
                new PlatformClearanceCheck(_landingContext),
                new CheckHistory(_landingContext)
            };
        }

        public IPhaseResponse Process(ILogger logger, IVectorElement<int> requestedPosition)
        {
            _landingContext.RequestedPosition = requestedPosition;
            foreach (var landingPhase in Phases)
                if (_landingContext.IsExecutionNeeded)
                {
                    var phaseResponse = landingPhase.Process(logger);
                    _landingContext.AddPhaseResultToHistory(phaseResponse);
                }

            return _landingContext.FinalResult.IsPhaseFailed
                ? _landingContext.FinalResult
                : new PhaseResponse { OperaStatus = Status.OkForLanding };
        }
    }
}