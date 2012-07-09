using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Foundation;

namespace FatAttitude.Utilities.Metro.Mapping
{
    public interface IAnnotationMarker
    {
        Point PositionAnchor { get;  }
    }
}
