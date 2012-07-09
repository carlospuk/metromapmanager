using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bing.Maps;

namespace FatAttitude.Utilities.Metro.Mapping
{
    public interface IMapPolyline
    {
        IEnumerable<Location> Locations { get;  }
    }
}
