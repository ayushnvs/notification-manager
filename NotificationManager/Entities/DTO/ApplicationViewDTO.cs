namespace NotificationManager.Entities.DTO;

public class ApplicationViewDTO
{
    public string? PackageName { get; set; }
    public string? AppName { get; set; }
    public ImageSource? AppLogo { get; set; }
    public bool ShowDefaultAppIcon { get; set; } = false;
    public int Count { get; set; }
}
