using NotificationManager.ViewModel;

namespace NotificationManager.Views;

public partial class NotificationsPage : ContentPage
{
	private readonly NotificationsPageViewModel _viewModel;
	public NotificationsPage(NotificationsPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}