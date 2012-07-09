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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FatAttitude.Utilities.Metro.Mapping
{
    public sealed partial class MapCallout : UserControl
    {
        public MapCallout()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }


        #region Own data context - if required
        // Title
        public static readonly DependencyProperty TitleProperty
             = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(MapCallout), null);
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        // Subtitle
        public static readonly DependencyProperty SubtitleProperty
             = DependencyProperty.RegisterAttached("Subtitle", typeof(string), typeof(MapCallout), null);
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }
        // DetailText
        public static readonly DependencyProperty DetailTextProperty
             = DependencyProperty.RegisterAttached("DetailText", typeof(string), typeof(MapCallout), null);
        public string DetailText
        {
            get { return (string)GetValue(DetailTextProperty); }
            set { SetValue(DetailTextProperty, value); }
        }
        #endregion


    }
}
