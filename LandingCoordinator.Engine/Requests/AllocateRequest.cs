using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine.Requests
{
    public class AllocateRequest<T> : IAllocateRequest<T>
    {
        public int RocketId { get; init; }
        public int OperationId { get; init; }
        public IVectorElement<T> Position { get; init; }
    }
}