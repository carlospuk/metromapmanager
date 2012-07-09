using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Foundation;
using Bing.Maps;

namespace FatAttitude.Utilities.Metro.Mapping
{
    public static class MapExtensionMethods
    {
        /// <summary>
        /// Scroll the map by a given number of pixels in a certain direction.  
        /// </summary>
        /// <param name="map"></param>
        /// <param name="offset">The offset to scroll by - positive X values move right, positive Y values move down</param>
        public static void scrollBy(this Map map, Point offset)
        {
            Point pMapCenter;
            if (!map.TryLocationToPixel(map.Center, out pMapCenter)) return;

            Point pNewCenter = new Point(pMapCenter.X - offset.X, pMapCenter.Y - offset.Y);
            Location mapNewLocation;
            if (!map.TryPixelToLocation(pNewCenter, out mapNewLocation)) return;

            map.SetView(mapNewLocation);

        }
    }
}
