using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine.Vector
{
    public class VectorElement<T> : IVectorElement<T>
    {
        public T i { get; set; }
        public T j { get; set; }
    }
}