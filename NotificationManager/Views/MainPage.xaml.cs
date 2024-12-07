using NotificationManager.ViewModel;

namespace NotificationManager.Views;

public partial class MainPage : ContentPage
{
    private readonly NotificationViewModel _notificationViewModel;

    public MainPage(NotificationViewModel notificationViewModel)
    {
        InitializeComponent();
        _notificationViewModel = notificationViewModel;
        BindingContext = _notificationViewModel;
    }

    
}
