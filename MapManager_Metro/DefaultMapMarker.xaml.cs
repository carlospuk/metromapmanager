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


namespace FatAttitude.Utilities.Metro.Mapping
{
    public sealed partial class DefaultMapMarker : UserControl, IAnnotationMarker
    {
        public DefaultMapMarker()
        {
            this.InitializeComponent();
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
