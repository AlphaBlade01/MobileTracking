using Syncfusion.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTrackerServer.Models;

public class MarkerTooltipInfo : MapTooltipInfo
{
    public new required MobileMarker DataItem { get; set; }
}
