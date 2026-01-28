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

    public class SerializerTests
    {
        private readonly string _testFolder;
        private readonly string _testFileName = "test_profil.json";

        public SerializerTests()
        {
            // On utilise un dossier temporaire pour ne pas polluer les vraies données
            _testFolder = Path.GetTempPath();
        }

        [Fact]
        public void Serialize_Puis_Deserialize_RetourneObjetIdentique()
        {
            // 1. ARRANGE (On crée un profil de test)
            var profilInitial = new Profil(sexe: 1, poids: 80f, taille: 180, age: 30);

            // Nettoyage au cas où un vieux fichier traîne
            string cheminComplet = Path.Combine(_testFolder, _testFileName);
            if (File.Exists(cheminComplet)) File.Delete(cheminComplet);

            // 2. ACT
            // On sauvegarde
            Serializer.Serialize(_testFolder, _testFileName, profilInitial);

            // On recharge
            var profilRecupere = Serializer.Deserialize(_testFolder, _testFileName);

            // 3. ASSERT
            Assert.NotNull(profilRecupere);
            Assert.Equal(profilInitial.Sexe, profilRecupere.Sexe);
            Assert.Equal(profilInitial.Poids, profilRecupere.Poids);
            Assert.Equal(profilInitial.Taille, profilRecupere.Taille);
            Assert.Equal(profilInitial.Age, profilRecupere.Age);
            Assert.Equal(profilInitial.Img, profilRecupere.Img);
            Assert.Equal(profilInitial.Message, profilRecupere.Message);

            // Nettoyage après le test
            if (File.Exists(cheminComplet)) File.Delete(cheminComplet);
        }

        [Fact]
        public void Deserialize_FichierInexistant_RetourneNull()
        {
            // ACT
            var resultat = Serializer.Deserialize(_testFolder, "fichier_qui_n_existe_pas.json");

            // ASSERT
            Assert.Null(resultat);
        }
    }
}