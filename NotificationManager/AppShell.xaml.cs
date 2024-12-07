using NotificationManager.Views;

namespace NotificationManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        }
    }
}
