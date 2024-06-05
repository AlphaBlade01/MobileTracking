using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MobileTrackerClient.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public string serverIp;

    [RelayCommand]
    public void OnConnectTriggered()
    {

    }

    public MainViewModel()
    { }
}
