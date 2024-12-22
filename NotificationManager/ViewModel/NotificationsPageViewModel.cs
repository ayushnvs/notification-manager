using CommunityToolkit.Mvvm.ComponentModel;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotificationManager.ViewModel;

[QueryProperty(nameof(AppPackageName), "packageName")]
public class NotificationsPageViewModel : ObservableObject
{
    public string AppPackageName
    {
        set
        {
            LoadNotifications(value);
            _appPackageName = value;
        }
    }

    private string _appPackageName {get; set;}
    private readonly INotificationRepository _notificationRepository;
    private ObservableCollection<NotificationDBO> _notifications;
    public ICommand DeleteNotificationCommand { get; set; }
    public ICommand RefreshCommand { get; }
    public bool isRefreshing;
    public ObservableCollection<NotificationDBO> Notifications
    {
        get => _notifications;
        set => SetProperty(ref _notifications, value);
    }
    public bool IsRefreshing
    {
        get => isRefreshing;
        set
        {
            isRefreshing = value;
            OnPropertyChanged(nameof(IsRefreshing));
        }
    }

    public NotificationsPageViewModel(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;

        DeleteNotificationCommand = new Command<NotificationDBO>(DeleteNotification);

        RefreshCommand = new Command(() =>
        {
            LoadNotifications(_appPackageName);
            IsRefreshing = false;
        });
    }

    public async void LoadNotifications(string packageName)
    {
        List<NotificationDBO> notifs = await _notificationRepository.GetNotificationsAsync(packageName);
        Notifications = new ObservableCollection<NotificationDBO>(notifs);
    }

    public async void DeleteNotification(NotificationDBO notification)
    {
        await _notificationRepository.DeleteNotificationAsync(notification.Id);
        Notifications.Remove(notification);
    }
}
