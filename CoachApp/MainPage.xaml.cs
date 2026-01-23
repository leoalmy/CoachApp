using CoachModele;

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
        /// <summary>
        /// Initialise une nouvelle instance de la page principale.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
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
        private void btnCalculer_Clicked(object sender, EventArgs e)
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

                // 3. Affichage du résultat numérique et du message
                lblResultat.Text = string.Format("Votre IMG : {0:f2}% - {1}",
                                    leProfil.GetImg(),
                                    leProfil.GetMessage());

                // 4. Mise à jour de l'image en fonction du message
                // Les fichiers doivent être dans Resources/Images sans l'extension
                string nomImage = "";
                switch (leProfil.GetMessage())
                {
                    case "Trop maigre.":
                        nomImage = "smiley_tropmaigre";
                        break;
                    case "Parfait.":
                        nomImage = "smiley_parfait";
                        break;
                    case "Surpoids.":
                        nomImage = "smiley_surpoids";
                        break;
                }
                imgResultat.Source = nomImage;
            }
            catch (Exception)
            {
                // Gestion d'erreur en cas de saisie incorrecte (ex: texte au lieu de chiffre)
                DisplayAlert("Erreur", "Veuillez saisir des valeurs numériques valides.", "OK");
            }
        }
    }
}