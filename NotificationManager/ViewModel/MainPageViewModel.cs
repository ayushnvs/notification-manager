using CommunityToolkit.Mvvm.ComponentModel;
using NotificationManager.Entities.DTO;
using NotificationManager.Entities.Models;
using NotificationManager.Repository.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotificationManager.ViewModel;

public class MainPageViewModel : ObservableObject
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IApplicationRepository _applicationRepository;
    private ObservableCollection<ApplicationViewDTO> _appName;
    public Command<string> DeleteNotificationCommand { get; }
    public ICommand RefreshCommand { get; }
    public bool isRefreshing;
    public ObservableCollection<ApplicationViewDTO> AppNames
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

    public MainPageViewModel(INotificationRepository notificationRepository, IApplicationRepository applicationRepository) 
    {
        _notificationRepository = notificationRepository;
        _applicationRepository = applicationRepository;
        LoadApplications();
        RefreshCommand = new Command(() =>
        {
            LoadApplications();
            IsRefreshing = false;
        });
    }

    public async void LoadApplications()
    {
        List<ApplicationViewDTO> appLists = await _applicationRepository.GetAllApplicationAsync();
        AppNames = new ObservableCollection<ApplicationViewDTO>(appLists);
    }

    public async void DeleteNotification(ApplicationViewDTO notification)
    {
        // TODO: await _notificationRepository.DeleteNotificationAsync(notification.Id);
        AppNames.Remove(notification);
    }
}
