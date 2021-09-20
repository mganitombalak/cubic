namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface IVectorElement<T>
    {
        T i { get; set; }
        T j { get; set; }
    }
}