using CommunityToolkit.Mvvm.ComponentModel;
using MobileTrackerServer.Models;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace MobileTrackerServer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public Collection<MobileMarker> MapMarkers { get; set; }
    public MapLatLng Center { get; set; }
    public MapZoomPanBehavior ZoomPanBehavior { get; set; }

    public MainViewModel() 
    {
        MapMarkers = [];
        ZoomPanBehavior = new MapZoomPanBehavior
        {
            MaxZoomLevel = 19,
            ZoomLevel = 19,
            EnableZooming = true,
            EnablePanning = true
        };

        MobileMarker marker = new()
        {
            Latitude = 52.56902,
            Longitude = -1.82200,
            IconHeight = 20,
            IconWidth = 20,
            Name = "Samsung S24"
        };

        Center = new MapLatLng(marker.Latitude, marker.Longitude);
        MapMarkers.Add(marker);
    }
}
