namespace MauiAppCoach.View;

public partial class HistoPage : ContentPage
{
    private readonly SQLiteDb _sqliteDbCoach;
    public HistoPage(SQLiteDb sqliteDbCoach)
    {
        InitializeComponent();
        _sqliteDbCoach = sqliteDbCoach;
    }

    // Cette méthode est appelée automatiquement à chaque fois que la page s'affiche
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Récupération des données depuis la base de données
        List<Profil> recuperationProfils = await _sqliteDbCoach.GetAllProfilsAsync();

        // 2. Tri des données : Du plus récent au plus ancien
        // On utilise OrderByDescending sur la propriété Datemesure
        var profilsTries = recuperationProfils.OrderByDescending(p => p.Datemesure).ToList();

        // 3. Liaison avec le XAML
        // On crée un contexte de données qui contient la propriété "ListeProfils" attendue par le XAML
        BindingContext = new
        {
            ListeProfils = profilsTries
        };
    }
}