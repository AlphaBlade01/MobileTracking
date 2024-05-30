using MobileTrackerServer.Models.DTOs;
using System.Net;
using System.Text.Json;

namespace MobileTrackerServer.Logic.Listeners;

internal class MessageReceiveArgs(MarkerDTO marker) : EventArgs
{
    public MarkerDTO Marker = marker;
}

public class UpdateTrackerListener : BaseListener
{
    public override string PATH => "/update";
    internal static event EventHandler<MessageReceiveArgs> MessageReceived;

    public override Task Post(HttpListenerContext context)
    {
        if (!context.Request.HasEntityBody) BadRequest(context);
        
        string body;
        using (var reader = new StreamReader(context.Request.InputStream))
        {
            body = reader.ReadToEnd();
        }

        MarkerDTO markerDTO = JsonSerializer.Deserialize<MarkerDTO>(body);
        Console.WriteLine(MessageReceived);
        MessageReceived?.Invoke(this, new MessageReceiveArgs(markerDTO));

        context.Response.StatusCode = 200;
        context.Response.Close();
        return Task.CompletedTask;
    }

    public UpdateTrackerListener() { }
}
