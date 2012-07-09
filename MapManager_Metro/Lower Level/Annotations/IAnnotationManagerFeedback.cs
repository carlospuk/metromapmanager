using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;


namespace FatAttitude.Utilities.Metro.Mapping
{
    public interface IAnnotationManagerFeedback
    {
        IAnnotationMarker MarkerForAnnotation(IMapAnnotation annotation);
        void MapAnnotationClicked(IMapAnnotation annotation);
    }
}
