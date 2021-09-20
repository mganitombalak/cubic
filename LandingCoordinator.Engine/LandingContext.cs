using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LandingCoordinator.Abstractions.Interfaces;

namespace LandingCoordinator.Engine
{
    public class LandingContext
    {
        protected internal static LandingArea LandingArea;

        protected internal static ConcurrentBag<Tuple<int, int>> History = new();

        private readonly Stack<IPhaseResponse> phaseHistory;

        public LandingContext(ILandingAreaParameter landingAreaParamaters)
        {
            LandingArea = new LandingArea(landingAreaParamaters);
            phaseHistory = new Stack<IPhaseResponse>();
        }

        protected internal IVectorElement<int> RequestedPosition { get; set; }

        protected internal bool IsExecutionNeeded => phaseHistory.Count == 0 ||
                                                     !phaseHistory.Any(p =>
                                                         (p.RequestedPosition.i == RequestedPosition.i) &
                                                         (p.RequestedPosition.j == RequestedPosition.j)) ||
                                                     !phaseHistory.Peek().IsPhaseFailed;

        protected internal IPhaseResponse FinalResult => phaseHistory.Peek();

        protected internal void AddPhaseResultToHistory(IPhaseResponse phaseResponse)
        {
            phaseHistory.Push(phaseResponse);
        }
    }
}