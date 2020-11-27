using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ISSTracker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Text;

namespace ISSTracker.Infrastructure.Services
{
    

    /// <summary>
    /// Service for manage map configuration and update ISS position
    /// </summary>
    internal class MapService : IMapService
    {
        private GMapControl mapReference = null;
        private GMap.NET.WindowsForms.GMapOverlay markers = null;
        private GMapMarker marker;
        private int counter = 0;
        private int persistentMarkPeriod = 10;
        private PointLatLng previousPosition;
        private long previousTimestamp = 0;
        private bool initialized;

        public event PositionUpdatedEventHandler PositionUpdated;

        public bool IsInitialized => this.IsInitialized;

        public MapService()
        {
            this.initialized = false;
        }

        /// <summary>
        /// Initialize the map
        /// </summary>
        /// <param name="mapReference">a GMapControl used to render the map</param>
        public void InitMap(GMapControl mapReference, int persistentMarkPeriod)
        {
            if (mapReference is null)
            {
                throw new ArgumentException($"Parameter {nameof(mapReference)} cannot be null");
            }
            if(persistentMarkPeriod < 1)
            {
                throw new ArgumentException($"Parameter {nameof(persistentMarkPeriod)} must be greater than 0");
            }

            if (this.mapReference == null)
            {
                this.mapReference = mapReference;
            }
            else
            {
                throw new InvalidOperationException("Map reference already set");
            }

            if (!this.initialized)
            {
                //Set how many ticks need for push a persistent marker
                this.persistentMarkPeriod = persistentMarkPeriod;

                //Set Bing Maps
                mapReference.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
                mapReference.ShowCenter = false;
                mapReference.Zoom = 2;

                //Add ISS current position Marker
                markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                this.mapReference.Overlays.Add(markers);
                Bitmap icon = new Bitmap("Assets/iss.png");
                this.previousPosition = new PointLatLng(0, 0);
                this.marker = new GMarkerGoogle(this.previousPosition, icon);
                markers.Markers.Add(marker);
                this.initialized = true;
            }            
        }

        /// <summary>
        /// Set a new position for ISS
        /// </summary>
        /// <param name="position">The new position</param>
        /// <param name="timestamp">a timestamp of when the position was detected</param>
        public void SetNewPosition(PointLatLng position, long timestamp)
        {
            counter++;

            //Every persistentMarkPeriod ticks of the timer a persistent marker is added to the overlay
            if (counter % persistentMarkPeriod == 0)
            {
                GMapMarker marker = new GMarkerGoogle(position, GMarkerGoogleType.yellow_small);
                markers.Markers.Add(marker);
            }

            //Update the previous position and timestamp. This values are used to raise an updated position event
            this.previousPosition = this.marker.Position;
            long timeDiff = timestamp - previousTimestamp;
            this.previousTimestamp = timestamp;
            this.marker.Position = position;

            //Raise position updated event
            OnPositionUpdated(new PositionUpdatedEventArgs() { PreviousPosition = this.previousPosition, CurrentPosition = position, Timespan = timeDiff });
        }

        protected virtual void OnPositionUpdated(PositionUpdatedEventArgs e)
        {
            PositionUpdatedEventHandler handler = PositionUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }    
}
