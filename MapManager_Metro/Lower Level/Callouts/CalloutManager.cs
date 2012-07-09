using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.Foundation;

using Bing.Maps;

namespace FatAttitude.Utilities.Metro.Mapping
{
    internal class CalloutManager
    {
        
        // Private members
        Map map;
        
        MapLayer calloutLayer;
        MapCallout callout;

        const int CALLOUT_ANCHOR_X = 145;
        const int CALLOUT_ANCHOR_Y = 130;

        // Delegate methods
        public IAnnotationManagerFeedback myDelegate { get; set; }

        public CalloutManager(Map map)
        {
            // Map
            this.map = map;

            // Layer to hold the annotations
            calloutLayer = new MapLayer();
            map.Children.Add(calloutLayer);

            initialiseCallout();
        }

        private void initialiseCallout()
        {
            // There is only ever one callout
            callout = new MapCallout();
            callout.Visibility = Visibility.Collapsed;
            calloutLayer.Children.Add(callout);
        }
        

        public void displayCallout(IMapAnnotation annotation, IAnnotationMarker annotationElement)
        {
            // Assign this annotation as the data context of the callout - so it can get Title, Subtitle, etc
            callout.DataContext = annotation;


            // Position
            MapLayer.SetPosition(callout, new Location(annotation.Latitude, annotation.Longitude));

            // Callout anchor point also depends upon the anchor of the pushpin
            MapLayer.SetPositionAnchor(callout, new Windows.Foundation.Point(
                CALLOUT_ANCHOR_X + annotationElement.PositionAnchor.X, 
                CALLOUT_ANCHOR_Y + annotationElement.PositionAnchor.Y));
            

            // Todo: animated appearance 
            callout.Visibility = Visibility.Visible;

            Point offsetRequired;
            if (!isCalloutInView(out offsetRequired))
                map.scrollBy(offsetRequired);
                
        }
        public void hideCallout()
        {
            // Todo: animated disappearance

            callout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }



        bool isCalloutInView(out Point offsetRequired)
        {
            offsetRequired = new Point();
            Location calloutLocation = MapLayer.GetPosition(callout);
            Point calloutTranslation = MapLayer.GetPositionAnchor(callout);

            Point calloutPixelLocation;
            if (!map.TryLocationToPixel(calloutLocation, out calloutPixelLocation))
                throw new Exception("Cannot get location of callout ");

            // Where is the callout?
            
            Point calloutTopLeftLocation = new Point(calloutPixelLocation.X - calloutTranslation.X, calloutPixelLocation.Y - calloutTranslation.Y);
            Rect calloutRect = new Rect(calloutTopLeftLocation, new Size(MapConstants.CalloutWidth, MapConstants.CalloutHeight));
            

            // Work out if it is within the map boundary
            double offsetXRequired = 0;
            double offsetYRequired = 0;
            double mapWidth = map.ActualWidth;
            double mapHeight = map.ActualHeight;
            if (calloutRect.Left < 0)
                offsetXRequired = (0 - calloutRect.Left);
            else if (calloutRect.Right > mapWidth)
                offsetXRequired = (mapWidth - calloutRect.Right);
            if (calloutRect.Top < 0)
                offsetYRequired = (0 - calloutRect.Top);
            else if (calloutRect.Bottom > mapHeight)
                offsetYRequired = (mapHeight - calloutRect.Bottom);


            offsetRequired = new Point(offsetXRequired, offsetYRequired);

            return (offsetXRequired == 0) && (offsetYRequired == 0);
        }
    }
}
