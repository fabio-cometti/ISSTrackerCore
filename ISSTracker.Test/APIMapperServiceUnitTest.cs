using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Models;
using ISSTracker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace ISSTracker.Test
{
    public class APIMapperServiceUnitTest
    {
        [Fact]
        public void TestConversionIsFine()
        {
            //Arrange
            IAPIMapperService mapperService = new APIMapperService();
            
            IssApiResponse response = new IssApiResponse();
            response.iss_position = new IssPosition();
            response.iss_position.latitude = "80.0";
            response.iss_position.longitude = "160.0";

            //Act
            PointLatLng mapped = mapperService.Map(response);

            //Assert
            mapped.Should().NotBeNull();
            mapped.Lat.Should().Be(80);
            mapped.Lng.Should().Be(160);
        }

        [Fact]
        public void IssApiResponseShouldNotBeNull()
        {
            //Arrange
            IAPIMapperService mapperService = new APIMapperService();            

            //Act
            Action mapped = () => mapperService.Map(null);

            //Assert
            mapped.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CoordinatesShouldBeValid()
        {
            //Arrange
            IAPIMapperService mapperService = new APIMapperService();

            IssApiResponse response = new IssApiResponse();
            response.iss_position = new IssPosition();
            response.iss_position.latitude = "95.0";
            response.iss_position.longitude = "160.0";

            //Act
            Action mapped = () => mapperService.Map(response);

            //Assert
            mapped.Should().Throw<ArgumentException>();
        }
    }
}
