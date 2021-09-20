using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine
{
    public class LandingAreaParamaters : ILandingAreaParameter
    {
        public int Size { get; set; }
        public int PlatformSize { get; set; }
        public IVectorElement<int> PlatformPosition { get; set; }
        public string Name { get; set; }
    }
}