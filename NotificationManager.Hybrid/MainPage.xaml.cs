namespace NotificationManager.Hybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

#if ANDROID

    protected override void OnAppearing()
    {
        if (contentPage.Content is null)
        {
            var previousRootComponents = blazorWebView.RootComponents;

            blazorWebView = new() { HostPage = blazorWebView.HostPage };

            foreach (var rootComponent in previousRootComponents)
            {
                blazorWebView.RootComponents.Add(rootComponent);
            }

            contentPage.Content = blazorWebView;
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        contentPage.Content = null;
    }

#endif
}
