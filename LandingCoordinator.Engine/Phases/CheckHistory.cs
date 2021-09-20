using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine.Response;
using Microsoft.Extensions.Logging;

namespace LandingCoordinator.Engine.Phases
{
    public class CheckHistory : ILandingPhase
    {
        private readonly LandingContext _context;

        public CheckHistory(LandingContext context)
        {
            _context = context;
        }

        public IPhaseResponse Process(ILogger logger)
        {
            logger.LogInformation("History Check");
            var tempHistory = new ConcurrentBag<Tuple<int, int>>();
            Parallel.For(0, 3, t =>
            {
                var first = _context.RequestedPosition.i + t - 1;
                tempHistory.Add(new Tuple<int, int>(first, _context.RequestedPosition.j - 1));
                tempHistory.Add(new Tuple<int, int>(first, _context.RequestedPosition.j));
                tempHistory.Add(new Tuple<int, int>(first, _context.RequestedPosition.j + 1));
            });

            var historyExists = LandingContext.History.Any(x => tempHistory.Contains(x));
            if (!historyExists)
                foreach (var tuple in tempHistory)
                    LandingContext.History.Add(tuple);

            return new PhaseResponse
            {
                IsPhaseFailed = historyExists,
                Message = historyExists
                    ? $"Previous rocket already asked for ({_context.RequestedPosition.i.ToString()},{_context.RequestedPosition.j.ToString()})"
                    : string.Empty,
                OperaStatus = Status.Clash,
                RequestedPosition = _context.RequestedPosition
            };
        }
    }
}