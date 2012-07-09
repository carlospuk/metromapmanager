using System;

using FatAttitude.Utilities.Metro.Mapping;

namespace MapUtilitiesSample
{
    public class Restaurant : IMapAnnotation
    {
        #region IMapAnnotation
        // Position of the map marker
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        // Text for the callout
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string DetailText { get; set; }
        #endregion
    }
}
