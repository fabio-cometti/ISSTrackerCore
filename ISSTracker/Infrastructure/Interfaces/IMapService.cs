using GMap.NET;
using GMap.NET.WindowsForms;
using System;

namespace ISSTracker.Infrastructure.Interfaces
{
    public delegate void PositionUpdatedEventHandler(Object sender, PositionUpdatedEventArgs e);

    public class PositionUpdatedEventArgs : EventArgs
    {
        public PointLatLng PreviousPosition { get; set; }
        public PointLatLng CurrentPosition { get; set; }
        public long Timespan { get; set; }
    }

    /// <summary>
    /// Service for manage map configuration and update ISS position
    /// </summary>
    public interface IMapService
    {
        bool IsInitialized { get; }

        event PositionUpdatedEventHandler PositionUpdated;

        /// <summary>
        /// Initialize the map
        /// </summary>
        /// <param name="mapReference">a GMapControl used to render the map</param>
        void InitMap(GMapControl mapReference, int persistentMarkPeriod);

        /// <summary>
        /// Set a new position for ISS
        /// </summary>
        /// <param name="position">The new position</param>
        /// <param name="timestamp">a timestamp of when the position was detected</param>
        void SetNewPosition(PointLatLng position, long timestamp);
    }
}