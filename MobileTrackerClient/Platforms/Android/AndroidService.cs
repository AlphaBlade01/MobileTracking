using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using MobileTrackerClient.Logic.Services;

namespace MobileTrackerClient.Platforms.Android;

[Service]
public class AndroidService : Service
{
    public override IBinder? OnBind(Intent? intent)
    {
        return null;
    }

    private void StartService()
    {
        Notification notification = new NotificationCompat.Builder(this, TrackingService.NOTIFICATION_CHANNEL_ID)
            .SetAutoCancel(false)
            .SetOngoing(true)
            .SetContentTitle("Location Tracking")
            .SetContentText("Location is being monitored.")
            .SetSmallIcon(Resource.Drawable.notification_bg)
            .Build();

        StartForeground(TrackingService.NOTIFICATION_ID, notification);
        _ = Task.Run(TrackingService.StartService);
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        StartService();
        return StartCommandResult.NotSticky;
    }
}
