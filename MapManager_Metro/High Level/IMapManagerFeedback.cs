using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;

namespace FatAttitude.Utilities.Metro.Mapping
{
    public interface IMapManagerFeedback
    {
        IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation);

    }
}
