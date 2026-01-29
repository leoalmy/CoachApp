using Xunit;

namespace CoachTests
{
    public class ProfilTests
    {
        // --- Tests pour le calcul IMG - Femmes (sexe = 0) ---

        [Fact]
        public void Femme_MoinsVingtCinq_RetourneTropMaigre()
        {
            // Arrange - Ordre: (Id, Date, Poids, Taille, Age, Sexe)
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 45, uneTaille: 165, unAge: 25, unSexe: 0);

            // Act & Assert
            Assert.Equal("Trop maigre.", profil.Message);
            Assert.True(profil.Img < 25);
        }

        [Fact]
        public void Femme_Entre25et30_RetourneParfait()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 63, uneTaille: 165, unAge: 30, unSexe: 0);

            // Act & Assert
            Assert.Equal("Parfait.", profil.Message);
            Assert.InRange(profil.Img, 25, 30);
        }

        [Fact]
        public void Femme_PlusTrente_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 90, uneTaille: 160, unAge: 35, unSexe: 0);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.Message);
            Assert.True(profil.Img > 30);
        }

        // --- Tests pour le calcul IMG - Hommes (sexe = 1) ---

        [Fact]
        public void Homme_MoinsQuinze_RetourneTropMaigre()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 50, uneTaille: 180, unAge: 25, unSexe: 1);

            // Act & Assert
            Assert.Equal("Trop maigre.", profil.Message);
            Assert.True(profil.Img < 15);
        }

        [Fact]
        public void Homme_Entre15et20_RetourneParfait()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 72, uneTaille: 180, unAge: 30, unSexe: 1);

            // Act & Assert
            Assert.Equal("Parfait.", profil.Message);
            Assert.InRange(profil.Img, 15, 20);
        }

        [Fact]
        public void Homme_PlusVingt_RetourneSurpoids()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, unPoids: 100, uneTaille: 175, unAge: 35, unSexe: 1);

            // Act & Assert
            Assert.Equal("Surpoids.", profil.Message);
            Assert.True(profil.Img > 20);
        }
    }
}