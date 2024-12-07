using CommunityToolkit.Mvvm.ComponentModel;
using NotificationManager.Repository.Interfaces;

namespace NotificationManager.ViewModel;

public class NotificationViewModel : ObservableObject
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationViewModel(INotificationRepository notificationRepository) 
    {
        _notificationRepository = notificationRepository; 
    }
}
