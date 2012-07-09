metromapmanager
===============

Classes to extend the functionality of the C# .NET Bing Maps Control for Windows 8 Metro

overview
========
MapManager makes it easy to add annotations (pushpins) and callouts to a Bing Map object for Metro.  When tapped, the annotations automatically display callout balloons which scroll into view if partially obscured.

This is a new project and Issues / Pull Requests are welcome - I'm looking to add more features and functionality, all help welcome.


sample code
===========
A full sample project is included in the repository and is the reccommended way to get started.

usage
=====
Create a XAML page with a Bing Maps object on it, be sure to add your API_KEY.  Then in your code-behind page, create a new MapManager, passing it a reference to your Map.  

        
            mm = new MapManager(bingMap);
        

annotations
===========
A map annotation is any object that implements the interface IMapAnnotation, e.g.

    public class Restaurant : IMapAnnotation
    {
        // Position of the map marker
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        // Text for the callout
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string DetailText { get; set; }
        #endregion
    }


So, to add annotations to the map to represent a list of Restaurant objects, you would use:

            mm.addAnnotations(restaurants);
            

The annotations are added, along with appropriate callout balloons that display when tapped.



markers
=======
To use custom map markers, you may also optionally provide the MapManager with a reference to an object that implements the interface IMapMarkerSource.  (this can be the same XAML page if you wish)

            mm.markerSource = this;


This object provides the Map with a marker to display for a given annotation:

        public IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation)
        {
                // Use a square map marker
                return new SquareMapMarker();
        }


You could choose to display different markers based on the type of annotation by testing, e.g

        if (annotation is House)
            return new HouseMapMarker()
        else if (annotation is Hotel)
            return new HotelMapMaker()
        else...
        
           
The marker that this method returns can be any UIElement, but it must implement IAnnotationMarker; a simple interface that specifies the anchor point of the annotation.  For example, here's how you would make a standard UserControl into a valid map marker:

    public sealed partial class SquareMapMarker : UserControl, IAnnotationMarker
    {
        public SquareMapMarker()
        {
            this.InitializeComponent();
        }


        // Anchor to the center point
        public Point PositionAnchor
        {
            get
            {
                return new Point(10, 10); // center point
            }
        }
    }
    
    
If you don't choose to implement IMapMarkerSource, a default (ugly) marker is used.


callouts
========
Callouts appear automatically when a marker is tapped.  They scroll into view if partially off-screen.  
If the user taps the button on a callout, then the event Callout_ButtonTapped is fired by MapManager.

polylines
=========
If an object supports the interface IMapPolyline then you can add it directly to the MapManager and a polyline will be drawn onto the map connecting all the locations:

     mm.addPolyline(polylineObject, Windows.UI.Colors.Blue, 6);
     
The IMapPolyline interface is simply a public field 'Locations' that must return an IEnumerable<Location>, i.e. a collection of Bing Map Locations.
     
     

development
===========
At the moment, the XAML and some of the code is a little basic - all suggestions for improvement appreciated.

