using CommunityToolkit.Mvvm.ComponentModel;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Notifications;

namespace NotificationManager.ViewModel;

[QueryProperty(nameof(AppPackageName), "appName")]
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
    public Command<NotificationDBO> DeleteNotificationCommand { get; }
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
        RefreshCommand = new Command(() =>
        {
            LoadNotifications(_appPackageName);
            IsRefreshing = false;
        });
        // TODO: Complete Delete command function
        DeleteNotificationCommand = new Command<NotificationDBO>(DeleteNotification);
    }

    public async void LoadNotifications(string packageName)
    {
        Notifications = new ObservableCollection<NotificationDBO>(await _notificationRepository.GetNotificationsAsync(packageName));
    }

    public async void DeleteNotification(NotificationDBO notification)
    {
        await _notificationRepository.DeleteNotificationAsync(notification.Id);
        Notifications.Remove(notification);
    }
}
