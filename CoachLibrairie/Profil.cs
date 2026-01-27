using System;

namespace CoachLibrairie
{
    [Serializable]
    public class Profil
    {
        // Propriétés automatiques (indispensables pour la sérialisation JSON)
        public int Sexe { get; set; }    // 0 pour Femme, 1 pour Homme
        public float Poids { get; set; } // en Kg
        public int Taille { get; set; }  // en cm
        public int Age { get; set; }     // en années
        public float Img { get; set; }   // Indice de Masse Grasse
        public string Message { get; set; } // Résultat interprété

        /// <summary>
        /// Constructeur vide : INDISPENSABLE pour la désérialisation JSON
        /// </summary>
        public Profil() { }

        /// <summary>
        /// Constructeur principal : utilisé lors du clic sur le bouton "Calculer"
        /// </summary>
        public Profil(int sexe, float poids, int taille, int age)
        {
            this.Sexe = sexe;
            this.Poids = poids;
            this.Taille = taille;
            this.Age = age;
            this.CalculIMG();
            this.ResultatIMG();
        }

        /// <summary>
        /// Calcule l'indice de masse grasse
        /// </summary>
        private void CalculIMG()
        {
            // Conversion de la taille en mètres pour la formule
            float tailleMetre = (float)this.Taille / 100;

            // Formule : (1,2 * IMC) + (0,23 * Age) - (10,83 * Sexe) - 5,4
            this.Img = (float)((1.2 * this.Poids / (tailleMetre * tailleMetre)) + (0.23 * this.Age) - (10.83 * this.Sexe) - 5.4);
        }

        /// <summary>
        /// Interprète l'IMG et valorise le message
        /// </summary>
        private void ResultatIMG()
        {
            if (this.Sexe == 0) // Cas pour les femmes
            {
                if (this.Img < 25) this.Message = "Trop maigre.";
                else if (this.Img <= 30) this.Message = "Parfait.";
                else this.Message = "Surpoids.";
            }
            else // Cas pour les hommes
            {
                if (this.Img < 15) this.Message = "Trop maigre.";
                else if (this.Img <= 20) this.Message = "Parfait.";
                else this.Message = "Surpoids.";
            }
        }
    }
}