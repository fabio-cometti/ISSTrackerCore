using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ISSTracker.Infrastructure.Models;
using System.Net;
using Moq.Protected;
using System.Threading;

namespace ISSTracker.Test
{
    public class TrackerServiceUnitTest
    {
        [Fact]
        public async Task TestConstructor()
        {
            //Arrange
            string fakeURL = "http://fakeURL";

            //Act
            Action construct1 = () => new TrackerService(null, fakeURL);
            Action construct2 = () => new TrackerService(new HttpClient(), "");
            Action construct3 = () => new TrackerService(new HttpClient(), null);

            //Assert
            construct1.Should().Throw<ArgumentNullException>();
            construct2.Should().Throw<ArgumentNullException>();
            construct3.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task TestGetISSCurrentPositionOkResult()
        {
            //Arrange
            string fakeURL = "http://fakeURL";

            var handlerMock = mockHandler(fakeOKResponse());            
            var httpClient = new HttpClient(handlerMock);

            ITrackerService trackerService = new TrackerService(httpClient, fakeURL);

            //Act
            var apiResult = await trackerService.GetISSCurrentPosition();

            //Assert
            apiResult.Should().BeOfType<IssApiResponse>().And.NotBeNull();
            apiResult.timestamp.Should().Be(1600000000);
            apiResult.iss_position.Should().NotBeNull();
            apiResult.iss_position.latitude.Should().Be("45.0");
            apiResult.iss_position.longitude.Should().Be("13.0");
        }

        [Fact]
        public async Task TestNotSuccessStatusCode()
        {
            //Arrange
            string fakeURL = "http://fakeURL";

            var handlerMock = mockHandler(fakeKOResponse());
            var httpClient = new HttpClient(handlerMock);

            ITrackerService trackerService = new TrackerService(httpClient, fakeURL);

            //Act
            Func<Task> action = async () => await trackerService.GetISSCurrentPosition();

            //Assert
            action.Should()
                .Throw<Exception>()
                .WithMessage("Error calling ISS API")
                .WithInnerException<Exception>()
                .WithMessage("ISS API respond with a status error code of*");
        }

        [Fact]
        public async Task TestEmptyResponse()
        {
            //Arrange
            string fakeURL = "http://fakeURL";

            var handlerMock = mockHandler(fakeOKResponseWithEmptyContent());
            var httpClient = new HttpClient(handlerMock);

            ITrackerService trackerService = new TrackerService(httpClient, fakeURL);

            //Act
            Func<Task> action = async () => await trackerService.GetISSCurrentPosition();

            //Assert
            action.Should()
                .Throw<Exception>()
                .WithMessage("Error calling ISS API")
                .WithInnerException<Exception>()
                .WithMessage("Content response is Empty");
        }

        [Fact]
        public async Task TestDeserializationFailure()
        {
            //Arrange
            string fakeURL = "http://fakeURL";

            var handlerMock = mockHandler(fakeOKResponseWithWrongJSON());
            var httpClient = new HttpClient(handlerMock);

            ITrackerService trackerService = new TrackerService(httpClient, fakeURL);

            //Act
            Func<Task> action = async () => await trackerService.GetISSCurrentPosition();

            //Assert
            action.Should()
                .Throw<Exception>()
                .WithMessage("Error calling ISS API");                
        }

        private HttpMessageHandler mockHandler(HttpResponseMessage expectedResponse)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(expectedResponse);
            return handlerMock.Object;
        }

        private HttpResponseMessage fakeOKResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""message"": ""success"", ""timestamp"": 1600000000, ""iss_position"": {""longitude"": ""13.0"", ""latitude"": ""45.0""}}"),
            };
        }

        private HttpResponseMessage fakeKOResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest                
            };
        }

        private HttpResponseMessage fakeOKResponseWithEmptyContent()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            };
        }

        private HttpResponseMessage fakeOKResponseWithWrongJSON()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""timestamp"": ""I'm a wrong JSON""}")
            };
        }
    }
}
