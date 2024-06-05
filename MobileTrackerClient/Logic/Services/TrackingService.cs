using MobileTrackerClient.Logic.Interfaces;
using MobileTrackerClient.Models;
using MobileTrackerClient.Models.DTOs;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MobileTrackerClient.Logic.Services;

public static class TrackingService
{
    public static readonly string NOTIFICATION_CHANNEL_ID = "tracking_service";
    public static readonly int NOTIFICATION_ID = 1;

    private static readonly string BASE_URL = "https://localhost:3773";
    private static readonly HttpClient httpClient = new();
    private static string device_id = string.Empty;
    private static bool enabled = false;

    private static async Task UpdateLocation()
    {
        Location? location = await Geolocation.GetLocationAsync();
        if (location == null) return;

        MarkerDTO marker = new MarkerDTO()
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Guid = device_id,
            Name = "Sample Device"
        };
        HttpContent content = new StringContent(JsonSerializer.Serialize(marker), Encoding.UTF8, "application/json");
        await httpClient.PostAsync(BASE_URL + "/update", content);
    }

    public static async void StartService()
    {
        HttpResponseMessage response = await httpClient.GetAsync(BASE_URL + "/generate-key");
        device_id = await response.Content.ReadAsStringAsync();
        enabled = true;

        while (enabled)
        {
            await UpdateLocation();
            await Task.Delay(5000);
        }
    }

    public static void StopService()
    {
        enabled = false;
    }
}
