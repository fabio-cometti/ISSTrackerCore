using GMap.NET.WindowsForms;
using ISSTracker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;

namespace ISSTracker.Test
{
    public class MapServiceUnitTest
    {
        [Fact]
        public void TestInitMapWithNullGMapParameterMustFail()
        {
            //Arrange
            MapService mapService = new MapService();

            //Act
            Action action = () => mapService.InitMap(null, 30);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage("Parameter*cannot be null");
        }

        [Fact]
        public void TestInitMapWithNegativePeriodMustFail()
        {
            //Arrange
            MapService mapService = new MapService();
            GMapControl map = new GMapControl();

            //Act
            Action action = () => mapService.InitMap(map, -10);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage("Parameter*must be greater than 0");
        }

        [Fact]
        public void TestInitMapTwiceMustFail()
        {
            //Arrange
            MapService mapService = new MapService();
            GMapControl map = new GMapControl();

            //Act
            mapService.InitMap(map, 30);
            Action action = () => mapService.InitMap(map, 30);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage("Map reference already set");
        }

        [Fact]
        public void TestInitMapMustWorksFine()
        {
            //Arrange
            MapService mapService = new MapService();
            var map = new Mock<GMapControl>();

            //Act
            mapService.InitMap(map.Object, 30);            

            //Assert
            map.Object.MapProvider.Should().BeOfType(typeof(GMap.NET.MapProviders.BingSatelliteMapProvider));
            map.Object.ShowCenter.Should().BeFalse();
            map.Object.Zoom.Should().Be(2.0);
            map.Object.Overlays.Should().HaveCount(1);
            map.Object.Overlays[0].Markers.Should().HaveCount(1);
            map.Object.Overlays[0].Markers[0].Position.Lat.Should().Be(0.0);
            map.Object.Overlays[0].Markers[0].Position.Lng.Should().Be(0.0);
        }

        [Fact]
        public void TestSetNewPosition()
        {
            //Arrange
            MapService mapService = new MapService();
            var map = new Mock<GMapControl>();
            mapService.InitMap(map.Object, 15);
            long initiaTimestamp = 1600000000;

            using (var monitoredSubject = mapService.Monitor())
            {
                //Act
                for (int i = 0; i < 30; i++)
                {
                    mapService.SetNewPosition(new GMap.NET.PointLatLng() { Lat = i, Lng = i }, initiaTimestamp + (5 * i));
                }

                //Assert            
                map.Object.Overlays[0].Markers.Should().HaveCount(3);
                map.Object.Overlays[0].Markers[0].Position.Lat.Should().Be(29.0);
                map.Object.Overlays[0].Markers[0].Position.Lng.Should().Be(29.0);
                map.Object.Overlays[0].Markers[1].Position.Lat.Should().Be(14.0);
                map.Object.Overlays[0].Markers[1].Position.Lng.Should().Be(14.0);
                map.Object.Overlays[0].Markers[2].Position.Lat.Should().Be(29.0);
                map.Object.Overlays[0].Markers[2].Position.Lng.Should().Be(29.0);
                monitoredSubject.Should().Raise("PositionUpdated");
            }
            
        }
    }
}
