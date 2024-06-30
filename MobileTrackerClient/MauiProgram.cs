using Microsoft.Extensions.Logging;
using MobileTrackerClient.ViewModels;
using MobileTrackerClient.Views;

namespace MobileTrackerClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services
                .AddSingleton<MainPage>()
                .AddSingleton<MainViewModel>();

#if ANDROID26_0_OR_GREATER
            builder.Services.AddTransient<Platforms.Android.AndroidService>();
#endif

            return builder.Build();
        }
    }
}
