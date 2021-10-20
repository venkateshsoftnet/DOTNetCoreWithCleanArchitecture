using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Application;
using TFLAssessment.Application.Interfaces;
using TFLAssessment.Domain.Entities;
using Xunit;
using static TFLAssessment.Application.RoadStatusQuery;

namespace TFLAssessment.UnitTests
{
    public class RoadStatusQueryṬest
    {
        private readonly Mock<IRoadClient> mockRoadClient = new Mock<IRoadClient>();
        private readonly IMapper mockMapper;

        public RoadStatusQueryṬest()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
                    cfg.AddMaps(Assembly.Load("TFLAssessment.Application")));
            mockMapper = new Mapper(configuration);
        }

        [Fact]
        public void Constructor_IsValid()
        {
            //act
            var roadStatusQueryHandler = new RoadStatusQueryHandler(mockRoadClient.Object, mockMapper);

            //assert
            Assert.NotNull(roadStatusQueryHandler);
            Assert.IsType<RoadStatusQueryHandler>(roadStatusQueryHandler);
        }

        [Fact]
        public async Task RoadStatusQuery_Handle_Success()
        {
            //Arrange
            List<Road> roads = new List<Road>
            {
                new Road() 
                {
                    Bounds="20",
                    DisplayName="test name",
                    StatusSeverityDescription="desc test",
                    Envelope="test env",
                    Url="test url",
                    StatusSeverity="test",
                    Id="1"
                }
            };
            mockRoadClient.Setup(result => result.GetRoadStatusAsync(It.IsAny<RoadStatusQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(roads));
            var request = new RoadStatusQuery
            {
                RoadIds = new List<string> { "A1", "A2" }
            };
            var roadStatusQueryHandler = new RoadStatusQueryHandler(mockRoadClient.Object, mockMapper);

            // Act
            var response = await roadStatusQueryHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<List<RoadStatusResponse>>(response);
            Assert.NotNull(response);
            Assert.True(response.Any());
        }

        [Fact]
        public void RoadStatusQuery_Handle_Failure()
        {
            // Arrange

            mockRoadClient.Setup(result => result.GetRoadStatusAsync(It.IsAny<RoadStatusQuery>(), It.IsAny<CancellationToken>()))
                 .Throws<Exception>();

            var roadStatusQueryHandler = new RoadStatusQueryHandler(mockRoadClient.Object, mockMapper);

            var request = new RoadStatusQuery
            {
                RoadIds = new List<string> { "A1", "A2" }
            };
            // Act & Assert       
            Assert.ThrowsAsync<Exception>(() => roadStatusQueryHandler.Handle(request, CancellationToken.None));
        }
    }
}
