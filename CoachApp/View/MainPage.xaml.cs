namespace MauiAppCoach.View
{
    /// <summary>
    /// Page principale de l'application CoachAppV1.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private Profil? leProfil;
        private readonly SQLiteDb _sqliteDbCoach; // Utilisation de l'instance injectée

        /// <summary>
        /// Initialise une nouvelle instance de la page principale avec injection de la BDD.
        /// </summary>
        /// <param name="database">L'instance de SQLiteDb fournie par MauiProgram</param>
        public MainPage(SQLiteDb database)
        {
            InitializeComponent();

            // On récupère la base de données injectée
            _sqliteDbCoach = database;

            // Chargement des données au démarrage
            SQLiteSelect();
        }

        private async void BtnCalculer_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Récupération et conversion des valeurs
                int poids = int.Parse(entPoids.Text);
                int taille = int.Parse(entTaille.Text);
                int age = int.Parse(entAge.Text);
                int sexe = rbHomme.IsChecked ? 1 : 0;

                // 2. Création de l'objet Profil
                leProfil = new Profil(null, DateTimeOffset.Now, poids, taille, age, sexe);

                // 3. Insertion asynchrone dans la base de données
                await _sqliteDbCoach.SaveProfilAsync(leProfil);

                // 4. Affichage du résultat
                await AffichageResultatAsync(leProfil);
            }
            catch (FormatException)
            {
                await DisplayAlert("Erreur", "Veuillez saisir des nombres entiers valides.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "Une erreur est survenue : " + ex.Message, "OK");
            }
        }

        private async Task AffichageResultatAsync(Profil profil)
        {
            if (profil == null) return;

            // Préparation des animations
            imgResultat.Opacity = 0;
            lblResultat.Opacity = 0;

            lblResultat.Text = $"Votre IMG : {profil.Img:f2}% - {profil.Message}";

            if (profil.Message == "Parfait.")
            {
                lblResultat.TextColor = Colors.Green;
                imgResultat.Source = "smiley_parfait";

                // Séquence de vibration pour succès
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1500));
                await Task.Delay(100);
                Vibration.Default.Cancel();
            }
            else
            {
                lblResultat.TextColor = Colors.Red;
                imgResultat.Source = profil.Message == "Trop maigre." ? "smiley_tropmaigre" : "smiley_surpoids";

                // Séquence de vibration pour alerte
                // Première vibration
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1001));
                await Task.Delay(200);
                Vibration.Default.Cancel();

                await Task.Delay(150); // Petite pause entre les deux

                // Deuxième vibration
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1001));
                await Task.Delay(200);
                Vibration.Default.Cancel();
            }

            await Task.WhenAll(
                imgResultat.FadeTo(1, 600),
                lblResultat.FadeTo(1, 600)
            );
        }

        private async void SQLiteSelect()
        {
            // On récupère le dernier profil depuis la base de données
            leProfil = await _sqliteDbCoach.GetLastProfilAsync();

            if (leProfil != null)
            {
                entPoids.Text = leProfil.Poids.ToString();
                entTaille.Text = leProfil.Taille.ToString();
                entAge.Text = leProfil.Age.ToString();

                if (leProfil.Sexe == 1)
                    rbHomme.IsChecked = true;
                else
                    rbFemme.IsChecked = true;

                // On attend que la page soit prête pour lancer l'affichage
                await AffichageResultatAsync(leProfil);
            }
        }
    }
}