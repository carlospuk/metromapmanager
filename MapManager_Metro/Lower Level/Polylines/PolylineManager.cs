using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI;
using Bing.Maps;

namespace FatAttitude.Utilities.Metro.Mapping
{
    internal class PolylineManager
    {
        

        // Private members
        Map map;
        
        MapShapeLayer polylinesLayer;
        Dictionary<IMapPolyline, MapPolyline> polylines;

        // Delegate methods


        internal PolylineManager(Map map)
        {
            this.map = map;

            polylines = new Dictionary<IMapPolyline, MapPolyline>();

            // Layer to hold the polylines
            polylinesLayer = new MapShapeLayer();
            map.ShapeLayers.Add(polylinesLayer);
        }

        internal void addPolyline(IMapPolyline polylineSource, Color color, double width)
        {
            // Does it already exist?
            if (polylines.ContainsKey(polylineSource))
                return;

            LocationCollection lc = new LocationCollection();
            foreach (Location loc in polylineSource.Locations)
                lc.Add(loc);

            // Add to the UI
            MapPolyline pl = new MapPolyline() { Locations = lc, Color = color, Width = width };
            polylinesLayer.Shapes.Add(pl);
            // And add to our location collection
            polylines.Add(polylineSource, pl);

            
        }
        internal void removePolyline(IMapPolyline polylineSource)
        {
            throw new NotImplementedException();
        }
        internal void removeAllPolylines()
        {
            // Remove from the UI
            foreach (MapPolyline pl in polylines.Values)
                polylinesLayer.Shapes.Remove(pl);

            // And clear our dictionary
            polylines.Clear();
        }


    }
}
