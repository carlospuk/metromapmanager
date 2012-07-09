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
To use custom map markers, you may also optionally provide the MapManager with a reference to an object that implements its feedback interface IMapManagerFeedback.  (this can be the same XAML page if you wish)

            mm.feedbackObject = this;


At the moment, the feedback interface consists of one simple method, which provides the Map with a marker to display for a given annotation:

        public IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation)
        {
                // Use a square map marker
                return new SquareMapMarker();
        ...



This marker can be any UIElement, but it must implement IAnnotationMarker; a simple interface that specifies the anchor point of the annotation.  For example:

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
    
    
If you don't choose to implement IMapManagerFeedback, a default marker is used.


callouts
========
Callouts appear automatically and scroll into view if partially off-screen.

At the moment, the XAML representing Callouts is very basic - all suggestions for improvement appreciated.

