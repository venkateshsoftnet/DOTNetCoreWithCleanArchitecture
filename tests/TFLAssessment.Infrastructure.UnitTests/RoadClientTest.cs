using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Application;
using TFLAssessment.Application.Exceptions;
using TFLAssessment.Domain.Entities;
using TFLAssessment.Infrastructure.Shared.HttpClients;
using Xunit;

namespace TFLAssessment.Infrastructure.UnitTests
{
    public class RoadClientTest
    {
        private readonly Mock<HttpClient> mockHttpClient = new Mock<HttpClient>();
        private readonly Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        private IOptions<ApiSettings> mockSecrets;

        [Fact]
        public void Constructor_IsValid()
        {
            //act
            mockSecrets = Options.Create(new ApiSettings());
            var roadClient = new RoadClient(mockHttpClient.Object, mockSecrets);

            //assert
            Assert.NotNull(roadClient);
            Assert.IsType<RoadClient>(roadClient);
        }

        [Fact]
        public void GetRoadStatus_ValidRoadId_ShouldReturnGenericExceptionResponse()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://mock.url")
            };

            var roads = new List<Road>()
            {
                new Road
                {
                    Id = "A1",
                    DisplayName = "Road Status is Good",
                    StatusSeverityDescription = "No Exceptional Delays"
                }
            };
            var json = JsonConvert.SerializeObject(roads);
            var resultContent = new StringContent(json);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = resultContent
            };

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .Throws(new ApiException());

            mockSecrets = Options.Create(new ApiSettings
            {
                AppId = "dummy app id",
                AppKey = "dummy app key",
                BaseURL = "https://api.tfl.gov.uk"
            });

            var roadClient = new RoadClient(httpClient, mockSecrets);

            var roadStatusQuery = new RoadStatusQuery()
            {
                RoadIds = new List<string> { "A1", "A2" }
            };

            var result = roadClient.GetRoadStatusAsync(roadStatusQuery, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            Assert.NotNull(result.Exception.Message);
            Assert.Contains(result.Exception.Message, "One or more errors occurred. (Unknown error)");
        }

        [Fact]
        public void GetRoadStatus_InValidRoadId_ShouldReturnApiExceptionResponse()
        {
            // Arrange            
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://mock.url")
            };
            var error = new Error()
            {
                message = "A233 is not a valid road"
            };

            var json = JsonConvert.SerializeObject(error);
            var resultContent = new StringContent(json);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = resultContent
            };

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            mockSecrets = Options.Create(new ApiSettings
            {
                AppId = "dummy app id",
                AppKey = "dummy app key",
                BaseURL = "https://api.tfl.gov.uk"
            });

            var roadClient = new RoadClient(httpClient, mockSecrets);

            var roadStatusQuery = new RoadStatusQuery()
            {
                RoadIds = new List<string> { "A1", "A2" }
            };

            var result = roadClient.GetRoadStatusAsync(roadStatusQuery, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
            Assert.NotNull(result.Exception.Message);
            Assert.Contains(result.Exception.InnerException.Message, error.message);
        }

        [Fact]
        public void GetRoadStatus_ValidRoadId_ShouldReturnSuccessResponse()
        {
            // Arrange            
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://mock.url")
            };

            var roads = new List<Road>()
            {
                new Road
                {
                    Id = "A1",
                    DisplayName = "Road Status is Good",
                    StatusSeverityDescription = "No Exceptional Delays"
                }
            };
            var json = JsonConvert.SerializeObject(roads, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var resultContent = new StringContent(json);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = resultContent
            };

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            mockSecrets = Options.Create(new ApiSettings
            {
                AppId = "dummy app id",
                AppKey = "dummy app key",
                BaseURL = "https://api.tfl.gov.uk"
            });

            var roadClient = new RoadClient(httpClient, mockSecrets);

            var roadStatusQuery = new RoadStatusQuery()
            {
                RoadIds = new List<string> { "A1", "A2" }
            };

            var result = roadClient.GetRoadStatusAsync(roadStatusQuery, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Road>>(result.Result);
            Assert.True(result.Result.Count == 1);
            Assert.Equal("A1", result.Result.FirstOrDefault().Id);
            Assert.Equal("Road Status is Good", result.Result.FirstOrDefault().DisplayName);
            Assert.Equal("No Exceptional Delays", result.Result.FirstOrDefault().StatusSeverityDescription);
        }

        [Fact]
        public void GetRoadStatus_MultipleValidRoadId_ShouldReturnSuccessResponse()
        {
            // Arrange            
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://mock.url")
            };

            var roads = new List<Road>()
            {
               new Road
                {
                    Id = "A1",
                    DisplayName = "Road Status is Good",
                    StatusSeverityDescription = "No Exceptional Delays"
                },
               new Road
                {
                    Id = "A2",
                    DisplayName = "Road Status is Good",
                    StatusSeverityDescription = "No Exceptional Delays"
                }
            };
            var json = JsonConvert.SerializeObject(roads, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var resultContent = new StringContent(json);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = resultContent
            };

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            mockSecrets = Options.Create(new ApiSettings
            {
                AppId = "dummy app id",
                AppKey = "dummy app key",
                BaseURL = "https://api.tfl.gov.uk"
            });

            var roadClient = new RoadClient(httpClient, mockSecrets);

            var roadStatusQuery = new RoadStatusQuery()
            {
                RoadIds = new List<string> { "A1", "A2" }
            };

            var result = roadClient.GetRoadStatusAsync(roadStatusQuery, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Road>>(result.Result);
            Assert.True(result.Result.Count == 2);
            Assert.Equal("A1", result.Result.FirstOrDefault().Id);
            Assert.Equal("Road Status is Good", result.Result.FirstOrDefault().DisplayName);
            Assert.Equal("No Exceptional Delays", result.Result.FirstOrDefault().StatusSeverityDescription);
        }

        [Fact]
        public void GetRoadStatus_EmptyRoadId_ShouldReturnFailureResponse()
        {
            // Arrange            
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://mock.url")
            };

            var roads = new List<Road>()
            {
                new Road()
            };
            var json = JsonConvert.SerializeObject(roads);
            var resultContent = new StringContent(json);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = resultContent
            };

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            mockSecrets = Options.Create(new ApiSettings
            {
                AppId = "dummy app id",
                AppKey = "dummy app key",
                BaseURL = "https://api.tfl.gov.uk"
            });

            var roadClient = new RoadClient(httpClient, mockSecrets);

            var roadStatusQuery = new RoadStatusQuery()
            {
                RoadIds = new List<string>()
            };

            var result = roadClient.GetRoadStatusAsync(roadStatusQuery, CancellationToken.None);

            //assert
            Assert.NotNull(result.Exception);
            Assert.NotNull(result.Exception.Message);
            Assert.Contains(result.Exception.InnerException.Message, "Invalid input");
        }
    }
}
