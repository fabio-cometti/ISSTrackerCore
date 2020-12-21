using FluentAssertions;
using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ISSTracker.Test
{
    public class CalculateSpeedUnitTest
    {
        [Theory]
        [MemberData(nameof(GetInvalidData))]        
        public void TestInvalidNegativeOrZeroValues(double distance, long timeDiff)
        {
            //Arrange
            SpeedService speedService = new SpeedService();

            //Act
            Action action = () => speedService.CalculateSpeed(distance, timeDiff);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage($"Invalid value of*for parameter*");
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public void TestCalculateSpeed(double distance, long timeDiff, double expectedSpeed)
        {
            //Arrange
            SpeedService speedService = new SpeedService();

            //Act
            double speed = speedService.CalculateSpeed(distance, timeDiff);

            //Assert
            speed.Should().Be(expectedSpeed);
        }

        [Fact]
        public void CalculateSpeedTest()
        {
            //Arrange
            long timeDiff = 3600;
            SpeedService speedService = new SpeedService();

            //IDistanceService distanceService = new DistanceService();
            PointLatLng nortPole = new PointLatLng(90, 0);
            PointLatLng greenwichOnEquator = new PointLatLng(0, 0);

            var distanceServiceMock = new Mock<IDistanceService>();
            distanceServiceMock
                .Setup(ds => ds.CalculateDistanceBetweenPoints(nortPole, greenwichOnEquator, 6376500, 412000))
                .Returns(10000000);

            var distanceService = distanceServiceMock.Object;

            double distance = distanceService.CalculateDistanceBetweenPoints(nortPole, greenwichOnEquator) / 1000;
            double expectedSpeed = 10000;

            //Act
            double speed = speedService.CalculateSpeed(distance, timeDiff);

            //Assert
            speed.Should().Be(10000);
        }

        public static IEnumerable<object[]> GetInvalidData()
        {
            var allData = new List<object[]>
            {
                new object[] { 0, 0 },
                new object[] { 0, 5 },
                new object[] { -1, 5 },
                new object[] { 1, 0 },
                new object[] { 1, -5 },
                new object[] { -1, -5 },
            };

            return allData;
        }

        public static IEnumerable<object[]> GetValidData()
        {
            var allData = new List<object[]>
            {
                new object[] { 10, 1, 36000 }
            };

            return allData;
        }
    }
}
