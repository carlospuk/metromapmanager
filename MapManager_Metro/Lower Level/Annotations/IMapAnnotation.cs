using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace FatAttitude.Utilities.Metro.Mapping
{
    public interface IMapAnnotation
    {
        string Title { get; }
        string Subtitle { get; }
        string DetailText { get; }
        double Longitude { get; }
        double Latitude { get; }



     
    }
}

