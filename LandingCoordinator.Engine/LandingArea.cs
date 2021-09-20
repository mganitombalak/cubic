using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine.Vector;
using MathNet.Numerics.LinearAlgebra;

namespace LandingCoordinator.Engine
{
    public record LandingArea
    {
        private Matrix<int> _zone;

        public LandingArea(ILandingAreaParameter paramaters)
        {
            Name = paramaters.Name;
            Size = paramaters.Size;
            PlatformSize = paramaters.PlatformSize;
            PlatformPosition = paramaters.PlatformPosition;
        }

        public string Name { get; init; }
        public int Size { get; init; }

        public int PlatformSize { get; init; }
        public IVectorElement<int> PlatformPosition { get; init; }

        protected internal IVectorElement<int> PlatformBoundaries => new VectorElement<int>
            { i = PlatformPosition.i + PlatformSize, j = PlatformPosition.j + PlatformSize };

        public Matrix<int> Zone => _zone ??= Matrix<int>.Build.Dense(PlatformSize, PlatformSize);
    }
}