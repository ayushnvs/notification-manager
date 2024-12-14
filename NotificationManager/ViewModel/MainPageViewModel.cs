using CommunityToolkit.Mvvm.ComponentModel;
using NotificationManager.Entities.DTO;
using NotificationManager.Repository.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotificationManager.ViewModel;

public class MainPageViewModel : ObservableObject
{
    private readonly INotificationRepository _notificationRepository;
    private ObservableCollection<NotificationCountDTO> _appName;
    public Command<string> DeleteNotificationCommand { get; }
    public ICommand RefreshCommand { get; }
    public bool isRefreshing;
    public ObservableCollection<NotificationCountDTO> AppNames
    {
        get => _appName;
        set => SetProperty(ref _appName, value);
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

    public MainPageViewModel(INotificationRepository notificationRepository) 
    {
        _notificationRepository = notificationRepository;
        LoadNotifications();
        RefreshCommand = new Command(() =>
        {
            LoadNotifications();
            IsRefreshing = false;
        });
    }

    public async void LoadNotifications()
    {
        List<NotificationCountDTO> appLists = await _notificationRepository.GetUniqueAppNamesAsync();
        AppNames = new ObservableCollection<NotificationCountDTO>(appLists);
    }

    public async void DeleteNotification(NotificationCountDTO notification)
    {
        // TODO: await _notificationRepository.DeleteNotificationAsync(notification.Id);
        AppNames.Remove(notification);
    }
}
