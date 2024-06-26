﻿using Microsoft.Extensions.Logging;
using MobileTrackerServer.Logic;
using MobileTrackerServer.Logic.Listeners;
using MobileTrackerServer.ViewModels;
using MobileTrackerServer.Views;
using Syncfusion.Maui.Core.Hosting;

namespace MobileTrackerServer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services
            .AddSingleton<MainViewModel>()
            .AddSingleton<MainPage>()
            .AddSingleton<GenerateKeyListener>()
            .AddSingleton<UpdateTrackerListener>()
            .AddSingleton<HttpServer>();

        builder.Services.BuildServiceProvider().GetService<HttpServer>(); // Hacky workaround to instantiate HttpServer class to make up for .NET Maui's lack of support for hosted services
        return builder.Build();
    }
}
