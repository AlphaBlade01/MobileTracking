using CommunityToolkit.Mvvm.ComponentModel;
using MobileTrackerServer.Logic.Listeners;
using MobileTrackerServer.Models;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace MobileTrackerServer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public Collection<MobileMarker> MapMarkers { get; set; }
    public MapLatLng Center { get; set; }
    public MapZoomPanBehavior ZoomPanBehavior { get; set; }

    public MainViewModel(UpdateTrackerListener listener) 
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
