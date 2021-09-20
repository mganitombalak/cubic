using System.Threading.Tasks;
using Orleans;

namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface ILandingOperation : IGrainWithIntegerKey
    {
        Task<bool> SetLandingAreParamters(ILandingAreaParameter landingAreParamaters);
        Task<IBaseResponse> AllocateForLanding(IAllocateRequest<int> allocationRequest);
    }
}