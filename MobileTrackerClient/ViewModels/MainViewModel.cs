using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MobileTrackerClient.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public string serverIp;

    [RelayCommand]
    public void ConnectTriggered()
    {
#if ANDROID26_0_OR_GREATER
        Android.Content.Intent intent = new(Android.App.Application.Context, typeof(Platforms.Android.AndroidService));
        intent.SetAction("START_SERVICE");
        Android.App.Application.Context.StartForegroundService(intent);
#endif
    }

    public MainViewModel()
    { }
}
