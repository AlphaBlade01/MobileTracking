using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;

namespace MobileTrackerServer.Logic.Listeners;

public class GenerateKeyListener : BaseListener
{
    private const int PORT = 3774;

    protected override async Task HandleResponse(string response)
    { }

    protected override async Task HandleClient(Socket handler)
    {
        string guid = Guid.NewGuid().ToString();
        await handler.SendAsync(Encoding.UTF8.GetBytes(guid));
    }

    public GenerateKeyListener(): base(PORT) { }
}
