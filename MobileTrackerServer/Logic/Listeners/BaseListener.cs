using System.Net;

namespace MobileTrackerServer.Logic.Listeners;

public abstract class BaseListener
{
    public abstract string PATH { get; }

    private void Default(HttpListenerContext context)
    {
        HttpListenerResponse response = context.Response;
        response.StatusCode = 501;
        response.Close();
    }

    protected static void BadRequest(HttpListenerContext ctx)
    {
        ctx.Response.StatusCode = 400;
        ctx.Response.Close();
    }

    public virtual async Task Post(HttpListenerContext context) => Default(context);
    public virtual async Task Get(HttpListenerContext context) => Default(context);
    public virtual async Task Put(HttpListenerContext context) => Default(context);
    public virtual async Task Delete(HttpListenerContext context) => Default(context);
}
