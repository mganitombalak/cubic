using System.Threading.Tasks;
using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine;
using LandingCoordinator.Engine.Response;
using Microsoft.Extensions.Logging;
using Orleans;

namespace LandingCoordinator.Operations
{
    public class LandingOperation : Grain, ILandingOperation
    {
        private readonly ILogger logger;
        private LandingEngine landingEngine;

        public LandingOperation(ILogger<LandingOperation> logger)
        {
            this.logger = logger;
        }

        public Task<bool> SetLandingAreParamters(ILandingAreaParameter landingAreaParameter)
        {
            landingEngine = new LandingEngine(landingAreaParameter);
            return Task.FromResult(true);
        }

        public Task<IBaseResponse> AllocateForLanding(IAllocateRequest<int> allocationRequest)
        {
            logger.LogInformation(
                $"\n Allocation request received from '{allocationRequest.RocketId.ToString()}' for {allocationRequest.OperationId.ToString()} on (x,y)={allocationRequest.Position.i.ToString()},{allocationRequest.Position.j.ToString()}");
            var response = landingEngine.Process(logger, allocationRequest.Position);
            IBaseResponse result = new AllocateResponse { Status = response.OperaStatus };
            return Task.FromResult(result);
        }
    }
}