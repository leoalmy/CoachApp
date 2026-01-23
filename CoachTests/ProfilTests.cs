using Xunit;
using CoachModele;

namespace Coach.Tests
{
    public class ProfilTests
    {
        // --- Tests pour le calcul IMG - Femmes (sexe = 0) ---

        [Fact]
        public void Femme_MoinsVingtCinq_RetourneTropMaigre()
        {
            // Arrange
            // Avec 45kg pour 1m65, l'IMG sera très bas
            var profil = new Profil(sexe: 0, poids: 45f, taille: 165, age: 25);

            // Act & Assert
            Assert.Equal("Trop maigre.", profil.GetMessage());
            Assert.True(profil.GetImg() < 25);
        }

        [Fact]
        public void Femme_Entre25et30_RetourneParfait()
        {
            // Arrange
            // 63kg, 1m65, 30 ans => IMG environ 28.5 (donc entre 25 et 30)
            var profil = new Profil(sexe: 0, poids: 63f, taille: 165, age: 30);

            // Act & Assert
            Assert.Equal("Parfait.", profil.GetMessage());
            Assert.InRange(profil.GetImg(), 25, 30);
        }

        [Fact]
        public void Femme_PlusTrente_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(sexe: 0, poids: 90f, taille: 160, age: 35);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.GetMessage());
            Assert.True(profil.GetImg() > 30);
        }

        // --- Tests pour le calcul IMG - Hommes (sexe = 1) ---

        [Fact]
        public void Homme_MoinsQuinze_RetourneTropMaigre()
        {
            // Arrange
            var profil = new Profil(sexe: 1, poids: 50f, taille: 180, age: 25);

            // Act & Assert
            Assert.Equal("Trop maigre.", profil.GetMessage());
            Assert.True(profil.GetImg() < 15);
        }

        [Fact]
        public void Homme_Entre15et20_RetourneParfait()
        {
            // Arrange
            // 72kg, 1m80, 30 ans => IMG environ 18.5 (Parfait pour un homme)
            var profil = new Profil(sexe: 1, poids: 72f, taille: 180, age: 30);

            // Act & Assert
            Assert.Equal("Parfait.", profil.GetMessage());
            Assert.InRange(profil.GetImg(), 15, 20);
        }

        [Fact]
        public void Homme_PlusVingt_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(sexe: 1, poids: 100f, taille: 175, age: 35);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.GetMessage());
            Assert.True(profil.GetImg() > 20);
        }
    }
}