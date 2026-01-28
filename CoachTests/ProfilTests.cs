using Xunit;
using CoachLibrairie;

namespace CoachTests
{
    public class ProfilTests
    {
        // --- Tests pour le calcul IMG - Femmes (sexe = 0) ---

        [Fact]
        public void Femme_MoinsVingtCinq_RetourneTropMaigre()
        {
            // Arrange
            var profil = new Profil(sexe: 0, poids: 45f, taille: 165, age: 25);

            // Act & Assert
            // On retire les parenthèses () car ce sont des propriétés maintenant
            Assert.Equal("Trop maigre.", profil.Message);
            Assert.True(profil.Img < 25);
        }

        [Fact]
        public void Femme_Entre25et30_RetourneParfait()
        {
            // Arrange
            var profil = new Profil(sexe: 0, poids: 63f, taille: 165, age: 30);

            // Act & Assert
            Assert.Equal("Parfait.", profil.Message);
            Assert.InRange(profil.Img, 25, 30);
        }

        [Fact]
        public void Femme_PlusTrente_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(sexe: 0, poids: 90f, taille: 160, age: 35);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.Message);
            Assert.True(profil.Img > 30);
        }

        // --- Tests pour le calcul IMG - Hommes (sexe = 1) ---

        [Fact]
        public void Homme_MoinsQuinze_RetourneTropMaigre()
        {
            // Arrange
            var profil = new Profil(sexe: 1, poids: 50f, taille: 180, age: 25);

            // Act & Assert
            Assert.Equal("Trop maigre.", profil.Message);
            Assert.True(profil.Img < 15);
        }

        [Fact]
        public void Homme_Entre15et20_RetourneParfait()
        {
            // Arrange
            var profil = new Profil(sexe: 1, poids: 72f, taille: 180, age: 30);

            // Act & Assert
            Assert.Equal("Parfait.", profil.Message);
            Assert.InRange(profil.Img, 15, 20);
        }

        [Fact]
        public void Homme_PlusVingt_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(sexe: 1, poids: 100f, taille: 175, age: 35);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.Message);
            Assert.True(profil.Img > 20);
        }
    }
}