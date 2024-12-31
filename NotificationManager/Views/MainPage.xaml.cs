using NotificationManager.Entities.DTO;
using NotificationManager.ViewModel;

namespace NotificationManager.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _mainPageViewModel;

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        _mainPageViewModel = mainPageViewModel;
        BindingContext = _mainPageViewModel;
    }

    private async void OnAppTapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid)
        {
            if (grid.BindingContext is ApplicationViewDTO tappedItem)
            {
                await Shell.Current.GoToAsync($"{nameof(NotificationsPage)}?packageName={tappedItem.PackageName}");
            }
        }
    }
}
