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
        MapCallout lastCallout = null;

        const int CALLOUT_ANCHOR_X = 145;
        const int CALLOUT_ANCHOR_Y = 130;

        // Delegate methods
        public event EventHandler<CalloutButtonTappedEventArgs> Callout_ButtonTapped;

        public CalloutManager(Map map)
        {
            // Map
            this.map = map;

            // Layer to hold the annotations
            calloutLayer = new MapLayer();
            map.Children.Add(calloutLayer);
        }



        #region Show/Hide
        public void displayCallout(IMapAnnotation annotation, IAnnotationMarker annotationElement)
        {
            // Hide any existing callout
            hideCallout();

            // There is only ever one callout
            MapCallout callout = new MapCallout();
            calloutLayer.Children.Add(callout);
            
            // Hook up tap event
            callout.Callout_Tapped += callout_Callout_Tapped;

            // Assign this annotation as the data context of the callout - so it can get Title, Subtitle, etc
            callout.DataContext = annotation;

            // Position
            MapLayer.SetPosition(callout, new Location(annotation.Latitude, annotation.Longitude));

            // Callout anchor point also depends upon the anchor of the pushpin
            MapLayer.SetPositionAnchor(callout, new Windows.Foundation.Point(
                CALLOUT_ANCHOR_X + annotationElement.PositionAnchor.X, 
                CALLOUT_ANCHOR_Y + annotationElement.PositionAnchor.Y));
            

            // Appear
            
            callout.animateOnscreen();

            Point offsetRequired;
            if (!isCalloutInView(callout, out offsetRequired))
                map.scrollBy(offsetRequired);

            // Store for later
            lastCallout = callout;
                
        }
        public void hideCallout()
        {
            if (lastCallout == null) return;

            // Unhook tapped event
            lastCallout.Callout_Tapped -= callout_Callout_Tapped;

            // Animate disappearance
            lastCallout.Callout_Disappeared += lastCallout_Callout_Disappeared;
            lastCallout.animateOffscreen();
            lastCallout = null;
        }

        void lastCallout_Callout_Disappeared(object sender, EventArgs e)
        {
            MapCallout co = (MapCallout)sender;
            co.Callout_Disappeared -= lastCallout_Callout_Disappeared;

            calloutLayer.Children.Remove(co);
        }
        #endregion

        // Tap Callout
        void callout_Callout_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (!(sender is MapCallout)) return;
            MapCallout c = (MapCallout)sender;
            if (!(c.DataContext is IMapAnnotation)) return;
            IMapAnnotation annotation = (IMapAnnotation)c.DataContext;

            if (this.Callout_ButtonTapped != null)
                this.Callout_ButtonTapped(this, new CalloutButtonTappedEventArgs(0, annotation));
        }

        // Helpers
        bool isCalloutInView(MapCallout callout, out Point offsetRequired)
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

    public class CalloutButtonTappedEventArgs : EventArgs
    {
        public int ButtonIndex { get; set; }
        public IMapAnnotation Annotation { get; set; }

        public CalloutButtonTappedEventArgs(int buttonIndex, IMapAnnotation annotation)
        {
            this.ButtonIndex = buttonIndex;
            this.Annotation = annotation;
        }
    }

}
