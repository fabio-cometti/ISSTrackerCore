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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }

        private void BtnStartTracking_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = ConfigurationManager.AppSettings["ISSApiURL"];
            ITrackerService trackerService = new TrackerService(client, url);
            IMapService mapService = new MapService();
            IAPIMapperService converter = new APIMapperService();
            IDistanceService distanceService = new DistanceService();
            ISpeedService speedService = new SpeedService();

            MapForm map = new MapForm(trackerService, mapService, converter, distanceService, speedService);
            map.Show();
        }
    }
}
