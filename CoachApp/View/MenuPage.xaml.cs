namespace MauiAppCoach.View;

public partial class MenuPage : ContentPage
{
    private readonly SQLiteDb _sqliteDbCoach;

    public MenuPage(SQLiteDb sqliteDbCoach)
    {
        InitializeComponent();
        _sqliteDbCoach = sqliteDbCoach;
    }

    private async void BtnCalculIMG_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(_sqliteDbCoach));
    }

    private async void BtnHistorique_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoPage(_sqliteDbCoach));
    }
}