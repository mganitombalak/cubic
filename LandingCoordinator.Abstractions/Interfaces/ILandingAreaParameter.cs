namespace LandingCoordinator.Abstractions.Interfaces
{
    public interface ILandingAreaParameter
    {
        int Size { get; set; }
        int PlatformSize { get; set; }
        IVectorElement<int> PlatformPosition { get; set; }
        string Name { get; set; }
    }
}