using CommunityToolkit.Mvvm.ComponentModel;
using MobileTrackerServer.Logic;
using MobileTrackerServer.Models;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace MobileTrackerServer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public Collection<MobileMarker> MapMarkers { get; set; }
    public MapLatLng Center { get; set; }
    public MapZoomPanBehavior ZoomPanBehavior { get; set; }

    public MainViewModel(NetworkListener listener) 
    {
        MapMarkers = [];
        ZoomPanBehavior = new MapZoomPanBehavior()
        {
            MaxZoomLevel = 19,
            ZoomLevel = 15,
            EnableDoubleTapZooming = true,
            MinZoomLevel = 5
        };
    }
}
