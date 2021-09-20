namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface IAvailabilityRequest<T>
    {
        int RocketId { get; init; }
        int OperationId { get; init; }
        IVectorElement<T> Position { get; init; }
    }
}