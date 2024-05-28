using MobileTrackerServer.ViewModels;

namespace MobileTrackerServer.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}
