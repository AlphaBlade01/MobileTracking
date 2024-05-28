using MobileTrackerServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobileTrackerServer.Logic.Listeners;

public abstract class BaseListener
{
    protected const string ACKNOWLEDGED = "<|ACK|>";
    protected Socket listener;

    protected abstract Task HandleResponse(string response);

    protected virtual async Task HandleClient(Socket handler)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int received = await handler.ReceiveAsync(buffer, SocketFlags.None);

            if (received == 0) // Client disconnected
                break;

            string rawResponse = Encoding.UTF8.GetString(buffer, 0, received);
            _ = Task.Run(() => HandleResponse(rawResponse));

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

    private async Task AsyncUpdateTrackerListener(int port)
    {
        IPHostEntry hosts = await Dns.GetHostEntryAsync("127.0.0.1");
        IPAddress localHost = hosts.AddressList[0];
        IPEndPoint endpoint = new(localHost, port);

        listener = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(endpoint);
        listener.Listen(100);
        BeginListen();
    }

    public BaseListener(int port) => AsyncUpdateTrackerListener(port);
}
