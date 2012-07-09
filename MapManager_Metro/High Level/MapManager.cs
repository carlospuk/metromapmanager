using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Private Members
        private Map map;
        AnnotationManager annotationManager;
        CalloutManager calloutManager;

        public MapManager(Map _map)
        {
            this.map = _map;
            annotationManager = new AnnotationManager(this.map );
            annotationManager.markerSource = this;
            annotationManager.feedbackObject = this;

            calloutManager = new CalloutManager(this.map);
        }

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
        #endregion





    }
}
