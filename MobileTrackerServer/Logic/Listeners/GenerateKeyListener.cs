using System.Diagnostics;
using System.Net;
using System.Text;

namespace MobileTrackerServer.Logic.Listeners;

public class GenerateKeyListener : BaseListener
{
    public override string PATH => "/generate-key";

    public override async Task Get(HttpListenerContext context)
    {
        Guid guid = Guid.NewGuid();
        HttpListenerResponse response = context.Response;

        try
        {
            byte[] buffer = Encoding.UTF8.GetBytes(guid.ToString());
            response.ContentType = "text/plain";
            response.ContentLength64 = buffer.Length;

            await response.OutputStream.WriteAsync(buffer);
            await response.OutputStream.FlushAsync();
        } catch (Exception ex)
        {
            Debug.WriteLine(ex);
        } finally
        {
            response.Close();
        }
    }

    public GenerateKeyListener() { }
}
