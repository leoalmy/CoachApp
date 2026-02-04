using CoachLibrairie;

namespace MauiAppCoach.View;

public partial class HistoPage : ContentPage
{
    public HistoPage()
    {
        InitializeComponent();
    }

    // Cette méthode est appelée automatiquement à chaque fois que la page s'affiche
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Récupération des données depuis la base de données
        var accesDistant = new AccesDistant();
        var profils = await accesDistant.RecupTousLesProfils();

        // 2. Tri des données : Du plus récent au plus ancien
        // On utilise OrderByDescending sur la propriété Datemesure
        var profilsTries = profils?.OrderByDescending(p => p.Datemesure).ToList() ?? new List<Profil>();

        // Appel de MajProfil() pour chaque profil pour recalculer l'IMG et le message
        foreach (var profil in profilsTries)
        {
            profil.MajProfil();
        }

        // 3. Liaison avec le XAML
        // On crée un contexte de données qui contient la propriété "ListeProfils" attendue par le XAML
        BindingContext = new
        {
            ListeProfils = profilsTries
        };
    }
}