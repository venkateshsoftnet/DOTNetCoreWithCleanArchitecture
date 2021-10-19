using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RoadStatus.Repository;
using RoadStatus.Service;
using RoadStatus.Service.ViewModels;

namespace RoadStatus.UnitTests
{
    public class RoadStatusTests
    {
        private Mock<IRoadStatusService> _mockService;
        private Mock<IConsoleLogger> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IRoadStatusService>();
            _mockLogger = new Mock<IConsoleLogger>();

        }


        [Test]
        public async Task GetRoadCorridorsAsync_ValidResponseReturnsSuccessCode()
        {
            // Given a valid road ID is specified
            CreateValidResponse();

            // When
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            var result = await sut.GetRoadStatusAsync("id1");

            // Then
            result.Should().Be(RoadStatusApp.ExitCodes.Success);
        }

        [Test]
        public async Task GetRoadCorridorsAsync_ValidResponse_ReturnsDisplayName()
        {
            // Given a valid road ID is specified
            CreateValidResponse();

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            await sut.GetRoadStatusAsync("id1");

            // Then the road ‘displayName’ should be displayed
            _mockLogger.Verify(x => x.WriteLine("The status of the road1 is as follows"), Times.Once);
        }

        [Test]
        public async Task GetRoadCorridorsAsync_ValidResponse_ReturnsStatusSeverity()
        {
            // Given a valid road ID is specified
            CreateValidResponse();

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            await sut.GetRoadStatusAsync("id1");

            // Then the road ‘statusSeverity’ should be displayed as ‘Road Status’
            _mockLogger.Verify(x => x.WriteLine("\tRoad Status is good"), Times.Once);
        }

        [Test]
        public async Task GetRoadCorridorsAsync_ValidResponse_ReturnsStatusDescription()
        {
            // Given a valid road ID is specified
            CreateValidResponse();

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            await sut.GetRoadStatusAsync("id1");

            // Then the road ‘statusSeverityDescription’ should be displayed as ‘Road Status Description’
            _mockLogger.Verify(x => x.WriteLine("\tRoad Status Description is very good"), Times.Once);
        }

        private void CreateValidResponse()
        {
            _mockService.Setup(x => x.GetRoadCorridorsAsync("id1"))
                .ReturnsAsync(new List<RoadCorridorViewModel>
                {
                    new RoadCorridorViewModel
                    {
                        Id = "id1",
                        DisplayName = "road1",
                        StatusSeverity = "good",
                        StatusSeverityDescription = "very good"
                    }
                });
        }

        [Test]
        public async Task GetRoadCorridorsAsync_NoRoadFoundReturnsInformativeError()
        {
            // Given an invalid road ID is specified
            _mockService.Setup(x => x.GetRoadCorridorsAsync("noSuchRoad"))
                .ThrowsAsync(new NoResultsFoundException());

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            var result = await sut.GetRoadStatusAsync("noSuchRoad");

            // Then the application should return an informative error
            _mockLogger.Verify(x => x.WriteLine($"noSuchRoad is not a valid road"), Times.Once);
        }

        [Test]
        public async Task GetRoadCorridorsAsync_NoRoadFoundReturnsNotFoundCode()
        {
            // Given an invalid road ID is specified
            _mockService.Setup(x => x.GetRoadCorridorsAsync("noSuchRoad"))
                .ThrowsAsync(new NoResultsFoundException());

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            var result = await sut.GetRoadStatusAsync("noSuchRoad");

            // Then the application should exit with a non-zero System Error code
            result.Should().Be(RoadStatusApp.ExitCodes.NotFound);
        }
        
        [Test]
        public async Task GetRoadCorridorsAsync_ErrorReturnsFailureCode()
        {
            // Given the service is down or broken
            _mockService.Setup(x => x.GetRoadCorridorsAsync("idx"))
                .ThrowsAsync(new Exception("service down"));

            // When the client is run
            var sut = new RoadStatusApp(_mockService.Object, _mockLogger.Object);
            var result = await sut.GetRoadStatusAsync("idx");

            // Then an error message is displayed
            result.Should().Be(RoadStatusApp.ExitCodes.Failure);
            _mockLogger.Verify(x => x.WriteLine($"Failure encountered: service down"), Times.Once);
        }
    }
}

