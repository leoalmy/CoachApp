using CoachLibrairie; 

namespace MauiAppCoach
{
    /// <summary>
    /// Page principale de l'application CoachAppV1.
    /// 
    /// Cette page contient l'interface utilisateur pour :
    /// - Saisir les données personnelles (poids, taille, âge, sexe)
    /// - Calculer l'Indice de Masse Grasse (IMG)
    /// - Afficher les résultats et interprétations
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private readonly string nomFichier = "saveprofil"; // Nom du fichier de sauvegarde pour la sérialisation
        /// <summary>
        /// Initialise une nouvelle instance de la page principale.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            RecupProfil();
        }

        /// <summary>
        /// Gère l'événement de clic sur le bouton "Calculer".
        /// 
        /// Cette méthode :
        /// 1. Récupère les valeurs saisies par l'utilisateur
        /// 2. Crée un objet Profil pour calculer l'IMG
        /// 3. Affiche les résultats avec un message personnalisé
        /// 4. Met à jour l'image selon le résultat
        /// 5. Gère les erreurs de saisie
        /// </summary>
        /// <param name="sender">L'objet qui a déclenché l'événement (le bouton)</param>
        /// <param name="e">Les arguments de l'événement</param>
        private async void BtnCalculer_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Récupération des valeurs saisies dans l'interface
                float poids = float.Parse(entPoids.Text);
                int taille = int.Parse(entTaille.Text);
                int age = int.Parse(entAge.Text);

                // Détermination du sexe (1 pour Homme, 0 pour Femme)
                int sexe = rbHomme.IsChecked ? 1 : 0;

                // 2. Création de l'objet Profil (le calcul se fait dans le constructeur)
                Profil leProfil = new(sexe, poids, taille, age);

                Serializer.Serialize(FileSystem.AppDataDirectory, nomFichier, leProfil); // Sauvegarde du profil

                await AffichageResultatAsync(leProfil);
            }
            catch (Exception)
            {
                // Gestion d'erreur en cas de saisie incorrecte (ex: texte au lieu de chiffre)
                await DisplayAlert("Erreur", "Veuillez saisir des valeurs numériques valides.", "OK");
            }
        }

        private async void RecupProfil()
        {
            try
            {
                Profil unProfil = null;
                unProfil = Serializer.Deserialize(FileSystem.AppDataDirectory, nomFichier);

                if (unProfil != null)
                {
                    entPoids.Text = unProfil.Poids.ToString();
                    entTaille.Text = unProfil.Taille.ToString();
                    entAge.Text = unProfil.Age.ToString();
                    rbHomme.IsChecked = (unProfil.Sexe == 1);
                    rbFemme.IsChecked = (unProfil.Sexe == 0);

                    await AffichageResultatAsync(unProfil);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "Erreur lors de la récupération des données", "Ok");
            }
        }

        private async Task AffichageResultatAsync(Profil leProfil)
        {
            // 1. Préparation (on cache pour l'animation)
            imgResultat.Opacity = 0;
            lblResultat.Opacity = 0;

            // 2. Mise à jour des contenus
            lblResultat.Text = $"Votre IMG : {leProfil.Img:f2}% - {leProfil.Message}";

            if (leProfil.Message == "Parfait.")
            {
                lblResultat.TextColor = Colors.Green;
                imgResultat.Source = "smiley_parfait";
                // Une petite vibration très courte pour le succès
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1001));
            }
            else
            {
                lblResultat.TextColor = Colors.Red;
                imgResultat.Source = leProfil.Message == "Trop maigre." ? "smiley_tropmaigre" : "smiley_surpoids";

                // --- LA VIBRATION "ALERTE" ---
                // On fait vibrer pendant 500ms si le résultat n'est pas "Parfait"
                // Première vibration longue de 2s
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1001));

                // On attend 2,2 secondes (le temps que la 1ère finisse + petite pause)
                await Task.Delay(1200);

                // Deuxième vibration longue de 2s
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1001));
            }

            // 3. Lancement des animations
            await Task.WhenAll(
                imgResultat.FadeTo(1, 600),
                lblResultat.FadeTo(1, 600)
            );
        }
    }
}