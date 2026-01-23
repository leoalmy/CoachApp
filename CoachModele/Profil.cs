namespace CoachModele
{
    public class Profil
    {
        // Propriétés privées
        private int sexe; // 0 pour une Femme et 1 pour un homme
        private float poids; // en Kg
        private int taille; // en cm
        private int age; // en années
        private float img; // Indice de Masse Grasse
        private string message = ""; // Annonce du résultat avec commentaire

        /// <summary>
        /// Constructeur : valorise les propriétés et lance les calculs
        /// </summary>
        public Profil(int sexe, float poids, int taille, int age)
        {
            this.sexe = sexe;
            this.poids = poids;
            this.taille = taille;
            this.age = age;
            this.CalculIMG(); // Appel auto lors de l'instanciation
            this.ResultatIMG(); // Appel auto lors de l'instanciation
        }

        /// <summary>
        /// Calcule l'indice de masse grasse
        /// Formule : (1,2 * Poids / Taille²) + (0,23 * Age) - (10,83 * S) - 5,4
        /// </summary>
        private void CalculIMG()
        {
            // Conversion de la taille en mètres pour la formule
            float tailleMetre = (float)this.taille / 100;
            this.img = (float)((1.2 * this.poids / (tailleMetre * tailleMetre)) + (0.23 * this.age) - (10.83 * this.sexe) - 5.4);
        }

        /// <summary>
        /// Interprète l'IMG et valorise le message
        /// </summary>
        private void ResultatIMG()
        {
            if (this.sexe == 0) // Cas pour les femmes
            {
                if (this.img < 25) this.message = "Trop maigre.";
                else if (this.img <= 30) this.message = "Parfait.";
                else this.message = "Surpoids.";
            }
            else // Cas pour les hommes
            {
                if (this.img < 15) this.message = "Trop maigre.";
                else if (this.img <= 20) this.message = "Parfait.";
                else this.message = "Surpoids.";
            }
        }

        // Getters
        public float GetImg() => img;
        public string GetMessage() => message;
    }
}