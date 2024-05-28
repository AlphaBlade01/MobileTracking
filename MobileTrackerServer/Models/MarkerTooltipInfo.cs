using Syncfusion.Maui.Maps;

namespace MobileTrackerServer.Models;

public class MarkerTooltipInfo : MapTooltipInfo
{
    public new required MobileMarker DataItem { get; set; }
}
