using Microsoft.EntityFrameworkCore;
using NotificationManager.Database;
using NotificationManager.Entities.Models;

namespace NotificationManager.Repository;

public class ApplicationRepository
{
    private readonly DatabaseContext _databaseContext;

    public ApplicationRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ApplicationDBO?> GetApplicationAsync(Guid id)
    {
        return await _databaseContext.Application.Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ApplicationDBO?> GetApplicationAsync(string packageName)
    {
        return await _databaseContext.Application.Where(i => i.Package == packageName).FirstOrDefaultAsync();
    }

    public async Task<int> SaveApplicationAsync(ApplicationDBO app)
    {
        await _databaseContext.Application.AddAsync(app);
        return await _databaseContext.SaveChangesAsync();
    }
}
