using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using FatAttitude.Utilities.Metro.Mapping;

namespace MapUtilitiesSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IMapMarkerSource
    {

        #region Sample Data
        List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant() {Title = "Jeff's Grill", Subtitle = "English", DetailText = "www.jeffsamazing-grill.com", Latitude=51.510699, Longitude=-0.135537},
                new Restaurant() {Title = "Gina's", Subtitle = "Italian", DetailText = "www.ginas-london-r.com", Latitude=51.510879, Longitude=-0.132608},
                new Restaurant() {Title = "Steak Thyme", Subtitle = "Argentinian", DetailText = "www.mysteakthyme.com", Latitude=51.509424, Longitude=-0.133778},
                new Restaurant() {Title = "Happy Burgers", Subtitle = "English", DetailText = "www.hap-burger.com", Latitude=51.509884, Longitude=-0.136213}
            };
        List<Museum> museums = new List<Museum>()
            {
                new Museum() {Title = "The Chess Museum", Subtitle = "Mon-Fri, 9am-5pm", DetailText = "www.chess-musem-uk.com", Latitude=51.511714, Longitude=-0.135312},
                new Museum() {Title = "National Toy Museum", Subtitle = "Mon-Sat, 10am-6pm", DetailText = "www.toy-museum-eg.com", Latitude=51.509043, Longitude=-0.133048}
            };
        #endregion

        // Our Map Manager
        MapManager mm;

        public MainPage()
        {
            this.InitializeComponent();

            // Initialise the MapManager, passing it a reference to our map
            mm = new MapManager(bingMap);
            // We are the source that says which map markers to use for each annotation
            mm.markerSource = this;
            // We're interested when a callout is tpped
            mm.Callout_ButtonTapped += mm_Callout_ButtonTapped;

            // Add our sample data to the map - annotations and callouts will appear automatically
            mm.addAnnotations(restaurants);
            mm.addAnnotations(museums);

            // You can also add polylines to the map from an object that supports the correct interface
            // mm.addPolyline(polylineSource, Windows.UI.Colors.Blue, 6);
        }



        // This method is where we tell the map what XAML UIElement to display for a given annotation
        public IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation)
        {
            if (annotation is Restaurant)
            {
                // Use a square map marker for restaurants
                SquareMapMarker squareMarker = new SquareMapMarker();

                // Number each map marker
                squareMarker.MarkerText = restaurants.IndexOf((Restaurant)annotation).ToString();

                return squareMarker;
            }
            else if (annotation is Museum)
            {
                // Use a square map marker for restaurants
                RoundMapMarker roundMarker = new RoundMapMarker();

                // Number each map marker
                roundMarker.MarkerText = museums.IndexOf((Museum)annotation).ToString();

                return roundMarker;
            }
            else
                return null;
        }


        // Callout button tapped
        void mm_Callout_ButtonTapped(object sender, CalloutButtonTappedEventArgs e)
        {
            // Show a message
            headerLabel.Text =  "You tapped on " + e.Annotation.Title;

            // You can Look at e.Annotation to figure out which object's callout was tapped
            if (e.Annotation is Restaurant)
            {
                Restaurant tappedRestaurant = (Restaurant)e.Annotation;
                // Do something with the object here
            }

        }



    }
}
