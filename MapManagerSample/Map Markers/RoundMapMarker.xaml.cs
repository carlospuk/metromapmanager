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
    public sealed partial class RoundMapMarker : UserControl, IAnnotationMarker
    {
        public RoundMapMarker()
        {
            this.InitializeComponent();

            // We are our own data context
            this.DataContext = this;
        }


        
        // DisplayText
        public static readonly DependencyProperty MarkerTextProperty
             = DependencyProperty.RegisterAttached("MarkerText", typeof(string), typeof(RoundMapMarker), null);
        public string MarkerText
        {
            get { return (string)GetValue(MarkerTextProperty); }
            set { SetValue(MarkerTextProperty, value); }
        }
        


        // Anchor to the center point
        #region IAnnotationMarker
        public Point PositionAnchor
        {
            get
            {
                return new Point(10, 10); // center point
            }
        }
        #endregion
    }
}
