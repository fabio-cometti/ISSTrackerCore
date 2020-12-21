using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace ISSTracker
{
    /// <summary>
    /// Form for tracking the ISS position. It render a map of the world and show where is the ISS in this moment
    /// </summary>
    public partial class MapForm : Form
    {
        private readonly ITrackerService trackerService;
        private readonly IMapService mapService;
        private readonly IAPIMapperService converter;
        private readonly IDistanceService distanceService;
        private readonly ISpeedService speedService;

        public MapForm(ITrackerService trackerService, 
            IMapService mapService, 
            IAPIMapperService converter, 
            IDistanceService distanceService, 
            ISpeedService speedService)
        {
            InitializeComponent();
            
            this.trackerService = trackerService;
            this.mapService = mapService;
            this.converter = converter;
            this.distanceService = distanceService;
            this.speedService = speedService;
            this.mapService.PositionUpdated += MapService_PositionUpdated;
        }

        private void MapForm_Load(object sender, System.EventArgs e)
        {
            //Init the map service
            int persistentMarkPeriod = Convert.ToInt32(ConfigurationManager.AppSettings["PersistentMarkPeriod"] ?? "15");
            this.mapService.InitMap(this.MainMap, persistentMarkPeriod);
            
            //Configure and start the refresh timer
            this.RefreshMapTimer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["TimerIntervalInSeconds"] ?? "10") * 1000;
            this.RefreshMapTimer.Start();
        }

        private async void RefreshMapTimer_Tick(object sender, EventArgs e)
        {
            var fromApiCurrentPosition = await trackerService.GetISSCurrentPosition().ConfigureAwait(true);            
            PointLatLng currentPosition = this.converter.Map(fromApiCurrentPosition);
            this.mapService.SetNewPosition(currentPosition, fromApiCurrentPosition.timestamp);
        }

        private void MapService_PositionUpdated(object sender, PositionUpdatedEventArgs e)
        {
            var distanceInKm = distanceService.CalculateDistanceBetweenPoints(e.PreviousPosition, e.CurrentPosition) / 1000;
            var interval = e.Timespan;
            var kmPerHour = this.speedService.CalculateSpeed(distanceInKm, interval);

            this.SpeedLabel.Text = $"Speed: {kmPerHour.ToString("0.00")} Km/h";

            this.LatitudeLabel.Text = $"Latitude: {e.CurrentPosition.Lat.ToString("0.000")}";
            this.LongitudeLabel.Text = $"Longitude: {e.CurrentPosition.Lng.ToString("0.000")}";
        }

        
    }
}