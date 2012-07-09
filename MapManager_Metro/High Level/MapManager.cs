using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI;
using Windows.UI.Xaml;


using Bing.Maps;

/*
 * Manages a Bing Map to add extra functionality:
 * 
 */

namespace FatAttitude.Utilities.Metro.Mapping
{
    public class MapManager : IAnnotationManagerFeedback, IMapMarkerSource
    {
        // Public members
        public IMapMarkerSource markerSource;
        // Events
        public event EventHandler<CalloutButtonTappedEventArgs> Callout_ButtonTapped;

        // Private Members
        private Map map;
        AnnotationManager annotationManager;
        CalloutManager calloutManager;
        PolylineManager polylineManager;

        public MapManager(Map _map)
        {
            // Map itself
            this.map = _map;
            _map.TappedOverride += _map_TappedOverride;

            annotationManager = new AnnotationManager(this.map );
            annotationManager.markerSource = this;
            annotationManager.feedbackObject = this;

            calloutManager = new CalloutManager(this.map);
            calloutManager.Callout_ButtonTapped += calloutManager_Callout_ButtonTapped;

            polylineManager = new PolylineManager(this.map);
        }






        #region Tap on Map
        void _map_TappedOverride(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            calloutManager.hideCallout();
        }
        #endregion


        #region Annotations
        /// <summary>
        /// Add a specified collection of annotations to the map
        /// </summary>
        /// <param name="annotations">The annotations to add</param>
        public void addAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationManager.addAnnotations(annotations);
        }
        /// <summary>
        /// Remove a specified collection of annotations from the map
        /// </summary>
        /// <param name="annotations">The annotations to remove</param>
        public void removeAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationManager.removeAnnotations(annotations);
        }
        /// <summary>
        /// Specify a collection of annotations that should remain on the map.
        /// New annotations are added to the map, and any annotations not in the specified collection are removed
        /// </summary>
        /// <param name="annotations">The annotations that should be on the map</param>
        public void setAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationManager.setAnnotations(annotations);
        }

        #region IAnnotationManagerDelegate

        public IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation)
        {
            // Calls back to our own delegate if it exists)
            if (markerSource != null)
                return markerSource.MarkerForAnnotation(annotation);
            else
                return new DefaultMapMarker();

        }
        public void MapAnnotationClicked(IMapAnnotation annotation)
        {
            displayCalloutForAnnotation(annotation);
        }
        #endregion
        
        #endregion

        #region Callouts
        void displayCalloutForAnnotation(IMapAnnotation annotation)
        {
            IAnnotationMarker annotationElement = (IAnnotationMarker)annotationManager.UIElementForAnnotation(annotation);
            
            calloutManager.displayCallout(annotation, annotationElement);
        }
        void calloutManager_Callout_ButtonTapped(object sender, CalloutButtonTappedEventArgs e)
        {
            if (this.Callout_ButtonTapped != null)
                this.Callout_ButtonTapped(this, e);
        }
        #endregion

        #region Polylines
        public void addPolyline(IMapPolyline polylineSource, Color color, double width)
        {
            polylineManager.addPolyline(polylineSource, color, width);
        }
        public void removePolyline(IMapPolyline polylineSource)
        {
            polylineManager.removePolyline(polylineSource);
        }
        public void removeAllPolylines()
        {
            polylineManager.removeAllPolylines();
        }
        #endregion



    }
}
