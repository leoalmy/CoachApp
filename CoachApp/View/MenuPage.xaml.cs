namespace MauiAppCoach.View;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
    }

    private async void BtnCalculIMG_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void BtnHistorique_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoPage());
    }
}