using System;

namespace MauiAppCoach.Modele
{
    /// <summary>
    /// Classe <c>Profil</c> pour le calcul et l'analyse de l'Indice de Masse Grasse (IMG).
    /// 
    /// Cette classe calcule automatiquement l'IMG d'une personne en fonction de paramètres personnels
    /// (sexe, poids, taille, âge) et fournit une interprétation du résultat.
    /// 
    /// Formule IMG : (1.2 × Poids / Taille²) + (0.23 × Âge) - (10.83 × Sexe) - 5.4
    /// 
    /// Les calculs sont effectués automatiquement lors de l'instanciation de la classe.
    /// </summary>
    public class Profil
    {
        // Propriétés privées
        /// <summary>Le sexe de la personne (0 = Femme, 1 = Homme)</summary>
        private int sexe;
        
        /// <summary>Le poids en kilogrammes</summary>
        private float poids;
        
        /// <summary>La taille en centimètres</summary>
        private int taille;
        
        /// <summary>L'âge en années</summary>
        private int age;
        
        /// <summary>L'Indice de Masse Grasse calculé</summary>
        private float img;
        
        /// <summary>Le message d'interprétation du résultat</summary>
        private string message = "";

        /// <summary>
        /// Initialise une nouvelle instance de la classe <c>Profil</c>.
        /// 
        /// Lors de la création, calcule automatiquement l'IMG et génère un message d'interprétation.
        /// </summary>
        /// <param name="sexe">Le sexe (0 pour Femme, 1 pour Homme)</param>
        /// <param name="poids">Le poids en kilogrammes</param>
        /// <param name="taille">La taille en centimètres</param>
        /// <param name="age">L'âge en années</param>
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
        /// Calcule l'Indice de Masse Grasse (IMG) selon la formule scientifique.
        /// 
        /// Formule appliquée : 
        /// IMG = (1.2 × Poids / Taille²) + (0.23 × Âge) - (10.83 × Sexe) - 5.4
        /// </summary>
        private void CalculIMG()
        {
            // Conversion de la taille en mètres pour la formule
            float tailleMetre = (float)this.taille / 100;
            this.img = (float)((1.2 * this.poids / (tailleMetre * tailleMetre)) + (0.23 * this.age) - (10.83 * this.sexe) - 5.4);
        }

        /// <summary>
        /// Interprète l'IMG calculé et génère un message de feedback personnalisé.
        /// 
        /// Les seuils d'interprétation diffèrent selon le sexe :
        /// - Femmes : Trop maigre (&lt;25), Parfait (25-30), Surpoids (&gt;30)
        /// - Hommes : Trop maigre (&lt;15), Parfait (15-20), Surpoids (&gt;20)
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
        /// <summary>
        /// Obtient l'Indice de Masse Grasse calculé.
        /// </summary>
        /// <returns>La valeur numérique de l'IMG</returns>
        public float GetImg() => img;
        
        /// <summary>
        /// Obtient le message d'interprétation du résultat IMG.
        /// </summary>
        /// <returns>Un message descriptif : "Trop maigre.", "Parfait." ou "Surpoids."</returns>
        public string GetMessage() => message;
    }
}