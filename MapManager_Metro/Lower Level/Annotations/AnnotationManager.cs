using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Bing.Maps;

namespace FatAttitude.Utilities.Metro.Mapping
{
    internal class AnnotationManager
    {

        // Private members
        Map map;
        
        MapLayer annotationsLayer;
        Dictionary<IMapAnnotation, UIElement> annotationElements;
        AnnotationCollection annotationCollection;

        // Delegate methods
        public IAnnotationManagerFeedback feedbackObject { get; set; }

        public AnnotationManager(Map map)
        {
            // Map
            this.map = map;

            // Collection to hold the annotation objects
            annotationCollection = new AnnotationCollection();
            annotationCollection.AnnotationsChanged += annotationCollection_AnnotationsChanged;

            // Layer to hold the annotations
            annotationsLayer = new MapLayer();
            map.Children.Add(annotationsLayer);

            // Pushpins to represent the annotations
            annotationElements = new Dictionary<IMapAnnotation, UIElement>();
        }

        // Public Methods
        public void addAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationCollection.addAnnotations(annotations);
        }
        public void removeAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationCollection.addAnnotations(annotations);
        }
        public void setAnnotations(IEnumerable<IMapAnnotation> annotations)
        {
            annotationCollection.setAnnotations(annotations);
        }

        // Incoming event - Annotations Changed
        void annotationCollection_AnnotationsChanged(object sender, AnnotationCollectionChangedEventArgs e)
        {
            foreach (IMapAnnotation item in e.annotationsAdded)
            {
                // We need the IAnnotationMarker to get the position anchor information
                IAnnotationMarker newAnnotationMarker = feedbackObject.MarkerForAnnotation(item);

                // Now cast to a normal UIElement
                UIElement newElement = (UIElement)newAnnotationMarker;

                Bing.Maps.Location loc = new Bing.Maps.Location(item.Latitude, item.Longitude );
                MapLayer.SetPosition(newElement, loc);
                MapLayer.SetPositionAnchor(newElement, newAnnotationMarker.PositionAnchor);

                annotationElements.Add(item, newElement);
                annotationsLayer.Children.Add(newElement);

                newElement.Tapped += annotation_Tapped;
            }
            foreach (IMapAnnotation item in e.annotationsRemoved)
            {
                UIElement oldPin;
                if (annotationElements.TryGetValue(item, out oldPin))
                {
                    annotationsLayer.Children.Remove(oldPin);
                    annotationElements.Remove(item);
                }
            }
        }

        void annotation_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Get annotation for the UIELement
            try
            {
                IMapAnnotation tappedAnnotation = annotationForUIElement((UIElement)sender);
                feedbackObject.MapAnnotationClicked(tappedAnnotation);
            }
            catch
            {
                return; // not found
            }

            
        }
        public UIElement UIElementForAnnotation(IMapAnnotation annotation)
        {
            return annotationElements[annotation];
        }
        public IMapAnnotation annotationForUIElement(UIElement element) {
            foreach (KeyValuePair<IMapAnnotation, UIElement> pair in annotationElements)
            {
                if (pair.Value.Equals(element)) return pair.Key;
            }

            throw new Exception("Value not found in the dictionary");
        }






    }
}
