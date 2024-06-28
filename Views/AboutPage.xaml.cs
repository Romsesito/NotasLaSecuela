namespace Notes.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }



    private async void github(object sender, EventArgs e)
    {
        // URL que deseas abrir
        string url = "https://github.com/Romsesito";

        // Verifica si el URL puede ser abierto y luego lo abre
        if (await Launcher.CanOpenAsync(url))
        {
            await Launcher.OpenAsync(url);
        }
    }

    private async void yutu(object sender, EventArgs e)
    {
        string url = "https://www.youtube.com/channel/UCe-tRcEe9RxDMvyRfXaZU9Q";
        if (await Launcher.CanOpenAsync(url))
        {
            await Launcher.OpenAsync(url);
        }
    }

    private async void steam(object sender, EventArgs e)
    {
        string url = "https://steamcommunity.com/profiles/76561198274277930/";
        if (await Launcher.CanOpenAsync(url))
        {
            await Launcher.OpenAsync(url);
        }
    }
}