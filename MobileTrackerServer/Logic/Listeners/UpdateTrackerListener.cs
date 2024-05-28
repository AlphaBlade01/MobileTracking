using MobileTrackerServer.Models.DTOs;
using System.Text.Json;

namespace MobileTrackerServer.Logic.Listeners;

public class UpdateTrackerListener : BaseListener
{
    private const int PORT = 3773;
    internal event MessageReceiveHandler MessageReceived;
    internal delegate void MessageReceiveHandler(MarkerDTO location);

    protected override Task HandleResponse(string response)
    {
        MarkerDTO markerDTO = JsonSerializer.Deserialize<MarkerDTO>(response);
        MessageReceived.Invoke(markerDTO);
        return Task.CompletedTask;
    }

    public UpdateTrackerListener() : base(PORT) { }
}
