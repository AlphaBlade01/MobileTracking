using CommunityToolkit.Mvvm.ComponentModel;
using MobileTrackerServer.Logic.Listeners;
using MobileTrackerServer.Models;
using MobileTrackerServer.Models.DTOs;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace MobileTrackerServer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public Collection<MobileMarker> mapMarkers;

    [ObservableProperty]
    public MapLatLng center;

    [ObservableProperty]
    public MapZoomPanBehavior zoomPanBehavior;

    [ObservableProperty]
    public bool visible;

    private void AddMarker(MarkerDTO markerDTO)
    {
        MobileMarker marker = new MobileMarker()
        {
            Latitude = markerDTO.Latitude,
            Longitude = markerDTO.Longitude,
            Name = markerDTO.Name,
            Guid = markerDTO.Guid,
        };
        MapMarkers.Add(marker);
        Center = new MapLatLng(markerDTO.Latitude, markerDTO.Longitude);
        Visible = true;
    }

    private void UpdateMarker(object? sender, MessageReceiveArgs eventArgs)
    {
        MarkerDTO markerDTO = eventArgs.Marker;
        if (!MapMarkers.Any(marker => marker.Guid == markerDTO.Guid))
        {
            AddMarker(markerDTO);
            return;
        }

        MobileMarker marker = MapMarkers.Single(m =>  m.Guid == markerDTO.Guid);
        marker.Latitude = markerDTO.Latitude;
        marker.Longitude = markerDTO.Longitude;
    }

    public MainViewModel() 
    {
        Visible = false;
        MapMarkers = [];
        ZoomPanBehavior = new MapZoomPanBehavior()
        {
            MaxZoomLevel = 19,
            ZoomLevel = 15,
            EnableDoubleTapZooming = true,
        };
        Center = new MapLatLng();

        UpdateTrackerListener.MessageReceived += UpdateMarker;
    }
}
