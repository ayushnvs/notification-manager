using NotificationManager.Hybrid.Entities.DTO;
using NotificationManager.Hybrid.Entities.Models;

namespace NotificationManager.Hybrid.Repository.Interfaces;

public interface IApplicationRepository
{
    Task<ApplicationDBO?> GetApplicationAsync(Guid id);
    Task<ApplicationDBO?> GetApplicationAsync(string packageName);
    Task<int> SaveApplicationAsync(ApplicationDBO app);
    Task<List<ApplicationViewDTO>> GetAllApplicationAsync();
    Task UpdateApplicationAsync(Guid id, DateTime? lastUpdated);
}
