using LandingCoordinator.Abstractions.Enums;
using LandingCoordinator.Engine.Vector;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LandingCoordinator.Engine.Test
{
    public class Tests
    {
        private LandingAreaParamaters _areaParamaters;
        private LandingEngine _landingEngine;
        private readonly ILogger _logger = Mock.Of<ILogger>();

        [SetUp]
        public void Setup()
        {
            _areaParamaters = new LandingAreaParamaters
            {
                Name = "Kennedy Space Station", Size = 100, PlatformPosition = new VectorElement<int> { i = 5, j = 5 },
                PlatformSize = 10
            };
            _landingEngine = new LandingEngine(_areaParamaters);
        }


        [Order(1)]
        [TestCase(16, 15, TestName = "ShouldLandingPermissionGrantedBetweenBoundaries",
            ExpectedResult = Status.OutOfPlatform)]
        [TestCase(5, 5, TestName = "ShouldLandingPermissionRevokedBetweenBoundaries",
            ExpectedResult = Status.OkForLanding)]
        [TestCase(5, 5, TestName = "ShouldLandingPermissionRevokedRequestedAgain", ExpectedResult = Status.Clash)]
        [TestCase(5, 5, TestName = "ShouldLandingPermissionRevokedRequestedAgain", ExpectedResult = Status.Clash)]
        [TestCase(5, 5, TestName = "ShouldLandingPermissionRevokedWhenTriedToLandPlatformSafeZone",
            ExpectedResult = Status.Clash)]
        public Status TestCommonCases(int x, int y)
        {
            return _landingEngine.Process(_logger, new VectorElement<int> { i = x, j = y }).OperaStatus;
        }

        [Order(2)]
        [TestCase(9, 9, TestName = "ShouldLandingPermissionGrantedToAnotherAvaliablePlatform",
            ExpectedResult = Status.OkForLanding)]
        public Status TestExceptionalCases(int x, int y)
        {
            return _landingEngine.Process(_logger, new VectorElement<int> { i = x, j = y }).OperaStatus;
        }
    }
}