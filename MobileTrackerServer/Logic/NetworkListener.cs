using MobileTrackerServer.Models.DTOs;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace MobileTrackerServer.Logic;

public class NetworkListener : IDisposable
{
    private const string ACKNOWLEDGED = "<|ACK|>";
    private Socket listener;
    internal event MessageReceiveHandler MessageReceived;

    internal delegate void MessageReceiveHandler(MarkerDTO location);

    private async Task HandleClient(Socket handler)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int received = await handler.ReceiveAsync(buffer, SocketFlags.None);

            if (received == 0) // Client disconnected
                break;

            string rawResponse = Encoding.UTF8.GetString(buffer, 0, received);
            MarkerDTO markerDTO = JsonSerializer.Deserialize<MarkerDTO>(rawResponse);
            MessageReceived.Invoke(markerDTO);

            await handler.SendAsync(Encoding.UTF8.GetBytes(ACKNOWLEDGED));
        }
    }

    private async Task BeginListen()
    {
        while (true)
        {
            Socket handler = await listener.AcceptAsync();
            _ = Task.Run(() => HandleClient(handler));
        }
    }

    private async Task AsyncNetworkListener()
    {
        IPHostEntry hosts = await Dns.GetHostEntryAsync("127.0.0.1");
        IPAddress localHost = hosts.AddressList[0];
        IPEndPoint endpoint = new(localHost, 3773);

        listener = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(endpoint);
        listener.Listen(100);
        BeginListen();
    }
    public NetworkListener() => AsyncNetworkListener();

    public void Dispose()
    {
        listener.Shutdown(SocketShutdown.Both);
    }
}
