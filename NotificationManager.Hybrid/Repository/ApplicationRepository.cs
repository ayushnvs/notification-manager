using Microsoft.EntityFrameworkCore;
using NotificationManager.Hybrid.Database;
using NotificationManager.Hybrid.Entities.DTO;
using NotificationManager.Hybrid.Entities.Models;
using NotificationManager.Hybrid.Repository.Interfaces;

namespace NotificationManager.Hybrid.Repository;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly INotificationRepository _notificationRepository;

    public ApplicationRepository(DatabaseContext databaseContext, INotificationRepository notificationRepository)
    {
        _databaseContext = databaseContext;
        _notificationRepository = notificationRepository;
    }

    public async Task<ApplicationDBO?> GetApplicationAsync(Guid id)
    {
        return await _databaseContext.Application.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<ApplicationDBO?> GetApplicationAsync(string packageName)
    {
        List<ApplicationDBO> applications = await _databaseContext.Application
        .Where(a => a.Package == packageName)
        .ToListAsync();

        return applications.FirstOrDefault();
    }

    public async Task<int> SaveApplicationAsync(ApplicationDBO app)
    {
        await _databaseContext.Application.AddAsync(app);
        return await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<ApplicationViewDTO>> GetAllApplicationAsync()
    {
        List<ApplicationDBO> applications = await _databaseContext.Application.OrderByDescending(app => app.LastUpdated).ToListAsync();

        List<ApplicationViewDTO> appsViewList = new List<ApplicationViewDTO>();
        foreach (ApplicationDBO? app in applications)
        {
            if (app == null) continue;

            ApplicationViewDTO notifCount = new ApplicationViewDTO()
            {
                PackageName = app.Package,
                AppName = app.Name,
                AppLogo = app.Icon != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(app.Icon)}" : null,
                //ShowDefaultAppIcon = app.Icon == null ? true : false,
                Count = await _notificationRepository.GetAppNotificationCount(app.Id)
            };

            if (notifCount.Count > 0) appsViewList.Add(notifCount);
        }

        return appsViewList;
    }

    public async Task UpdateApplicationAsync(Guid id, DateTime? lastUpdated)
    {
        ApplicationDBO? app = await GetApplicationAsync(id);
        if (app == null) return;
        app.LastUpdated = lastUpdated;
        _databaseContext.Application.Update(app);
        await _databaseContext.SaveChangesAsync();
    }
}
