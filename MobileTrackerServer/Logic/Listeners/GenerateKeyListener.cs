using System.Net;
using System.Text;

namespace MobileTrackerServer.Logic.Listeners;

public class GenerateKeyListener : BaseListener
{
    public override string PATH => "/generate-key";

    public override async Task Get(HttpListenerContext context)
    {
        HttpListenerResponse response = context.Response;
        Guid guid = Guid.NewGuid();
        await response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(guid.ToString()));
        response.Close();
    }

    public GenerateKeyListener() { }
}
