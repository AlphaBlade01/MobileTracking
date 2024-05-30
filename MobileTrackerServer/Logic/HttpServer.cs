using MobileTrackerServer.Logic.Listeners;
using System.Net;

namespace MobileTrackerServer.Logic;

public class HttpServer
{
    private const int PORT = 3773;
    private static readonly string URL = $"http://localhost:{PORT}/";
    private Dictionary<string, BaseListener> listeners;
    private HttpListener listener;

    private async Task HandleConnections()
    {
        while (true)
        {
            try
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                string? absolutePath = request?.Url?.AbsolutePath;
                BaseListener? handler = absolutePath != null ? listeners.GetValueOrDefault(absolutePath) : null;

                if (request == null || handler == null) continue;

                switch (request.HttpMethod)
                {
                    case "GET":
                        handler.Get(context);
                        break;
                    case "POST":
                        handler.Post(context);
                        break;
                    case "PUT":
                        handler.Put(context);
                        break;
                    case "DELETE":
                        handler.Delete(context);
                        break;
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    internal void RegisterListener(BaseListener listener)
    {
        listeners.Add(listener.PATH, listener);
    }

    public HttpServer(UpdateTrackerListener updateTrackerListener, GenerateKeyListener generateKeyListener)
    {
        listener = new HttpListener();
        listeners = [];
        
        RegisterListener(updateTrackerListener);
        RegisterListener(generateKeyListener);

        listener.Prefixes.Add(URL);
        listener.Start();

        Task.Run(HandleConnections);
    }
}
