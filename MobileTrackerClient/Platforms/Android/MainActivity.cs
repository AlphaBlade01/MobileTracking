using Android.App;
using Android.Content.PM;
using Android.OS;
using MobileTrackerClient.Logic.Services;

namespace MobileTrackerClient;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity Activity { get; set; }

    public MainActivity()
    {
        Activity = this;
    }

    private void CreateNotificationChannel(string channelId, string channelName, NotificationImportance importance, string description)
    {
#if ANDROID26_0_OR_GREATER
        NotificationChannel channel = new(channelId, channelName, importance)
        {
            Description = description
        };

        NotificationManager? notificationManager = (NotificationManager)GetSystemService(NotificationService);
        notificationManager?.CreateNotificationChannel(channel);
#endif
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        CreateNotificationChannel(TrackingService.NOTIFICATION_CHANNEL_ID, nameof(TrackingService), NotificationImportance.Default, "Service to track device");
    }
}
