using NotificationManager.Entities.DTO;
using NotificationManager.Entities.Models;

namespace NotificationManager.Repository.Interfaces;

public interface IApplicationRepository
{
    Task<ApplicationDBO?> GetApplicationAsync(Guid id);
    Task<ApplicationDBO?> GetApplicationAsync(string packageName);
    Task<int> SaveApplicationAsync(ApplicationDBO app);
    Task<List<ApplicationViewDTO>> GetAllApplicationAsync();
}
