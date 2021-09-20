using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine.Response
{
    public class AllocateResponse : IBaseResponse
    {
        public Status Status { get; set; }
    }
}