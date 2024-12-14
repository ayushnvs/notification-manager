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

    private async void OnAppSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listView.SelectedItem == null) return;
        await Shell.Current.GoToAsync($"{nameof(NotificationsPage)}?appName={((NotificationCountDTO)listView.SelectedItem).AppName}");
    }

    private void OnAppTapped(object sender, ItemTappedEventArgs e)
    {
        listView.SelectedItem = null;
    }
}
