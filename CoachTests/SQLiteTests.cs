using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CoachTests
{
    // On utilise IAsyncLifetime pour préparer la base en mémoire proprement
    public class SQLiteTests : IAsyncLifetime
    {
        private SQLiteAsyncConnection _connection;
        private SQLiteDb _db;

        public async Task InitializeAsync()
        {
            // Le nom spécial ":memory:" crée une base en RAM
            // FullMutex permet d'éviter des soucis de threading pendant les tests
            _connection = new SQLiteAsyncConnection(":memory:", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);

            // On initialise la structure de la base immédiatement
            await _connection.CreateTableAsync<Profil>();

            // On injecte cette connexion dans notre classe SQLiteDb
            _db = new SQLiteDb(_connection);
        }

        public async Task DisposeAsync()
        {
            // On ferme juste la connexion, la mémoire est libérée automatiquement
            await _connection.CloseAsync();
        }

        [Fact]
        public async Task SaveProfilAsync_NouveauProfil_InsertionReussie()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, 75, 180, 25, 1);

            // Act
            await _db.SaveProfilAsync(profil);
            var result = await _db.GetLastProfilAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(75, result.Poids);
        }

        [Fact]
        public async Task SaveProfilAsync_ProfilExistant_MiseAJourReussie()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, 70, 175, 30, 1);
            await _db.SaveProfilAsync(profil);

            // On récupère le profil avec son ID auto-généré
            var profilToUpdate = await _db.GetLastProfilAsync();
            profilToUpdate.Poids = 75; // On change le poids

            // Act
            await _db.SaveProfilAsync(profilToUpdate);
            var updatedProfil = await _db.GetLastProfilAsync();

            // Assert
            Assert.Equal(75, updatedProfil.Poids);
            Assert.Equal(profilToUpdate.Id, updatedProfil.Id);
        }

        [Fact]
        public async Task GetAllProfilsAsync_RetourneListeComplete()
        {
            // Arrange
            await _db.SaveProfilAsync(new Profil(null, DateTimeOffset.Now, 60, 160, 20, 0));
            await _db.SaveProfilAsync(new Profil(null, DateTimeOffset.Now, 80, 180, 40, 1));

            // Act
            List<Profil> result = await _db.GetAllProfilsAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetLastProfilAsync_RetourneDernierElementInsere()
        {
            // Arrange
            await _db.SaveProfilAsync(new Profil(null, DateTimeOffset.Now, 60, 160, 20, 0));
            await _db.SaveProfilAsync(new Profil(null, DateTimeOffset.Now, 99, 199, 99, 1)); // Le dernier

            // Act
            var last = await _db.GetLastProfilAsync();

            // Assert
            Assert.Equal(99, last.Poids);
        }

        [Fact]
        public async Task DeleteProfilAsync_SupprimeCorrectement()
        {
            // Arrange
            var profil = new Profil(null, DateTimeOffset.Now, 70, 175, 30, 1);
            await _db.SaveProfilAsync(profil);
            var inserted = await _db.GetLastProfilAsync();

            // Act
            int result = await _db.DeleteProfilAsync(inserted);
            var all = await _db.GetAllProfilsAsync();

            // Assert
            Assert.Equal(1, result);
            Assert.Empty(all);
        }


        // --- Tests de Robustesse / Cas limites ---

        [Fact]
        public async Task GetLastProfilAsync_BaseVide_RetourneNull()
        {
            // Act
            var result = await _db.GetLastProfilAsync();

            // Assert
            // Vérifie que le code ne crash pas s'il n'y a rien en base
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProfilAsync_ProfilInexistant_RetourneZero()
        {
            // Arrange
            var profilNonEnregistre = new Profil(null, DateTimeOffset.Now, 70, 175, 30, 1);
            // On s'assure que l'ID n'est pas en base
            profilNonEnregistre.Id = 999;

            // Act
            int result = await _db.DeleteProfilAsync(profilNonEnregistre);

            // Assert
            // SQLite retourne 0 si aucune ligne n'a été supprimée
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task SaveProfilAsync_MultiplesEntrees_ConserveIntegrite()
        {
            // Arrange
            int nombreEntrees = 10;

            // Act
            for (int i = 0; i < nombreEntrees; i++)
            {
                await _db.SaveProfilAsync(new Profil(null, DateTimeOffset.Now, 60 + i, 170, 25, 0));
            }

            var tousLesProfils = await _db.GetAllProfilsAsync();

            // Assert
            Assert.Equal(nombreEntrees, tousLesProfils.Count);
            // Vérifie que le dernier inséré est bien le dernier récupéré (Poids 69)
            Assert.Equal(69, (await _db.GetLastProfilAsync()).Poids);
        }

        [Fact]
        public async Task SaveProfilAsync_UpdateSurIdInexistant_NeCreePasDeDoublon()
        {
            // Arrange
            // On simule un profil qui a un ID positif mais n'existe pas en base
            var profilFantome = new Profil(null, DateTimeOffset.Now, 80, 180, 20, 1);
            profilFantome.Id = 500;

            // Act
            int result = await _db.SaveProfilAsync(profilFantome);

            // Assert
            // connection.UpdateAsync retourne 0 si l'ID n'existe pas
            Assert.Equal(0, result);
            var count = (await _db.GetAllProfilsAsync()).Count;
            Assert.Equal(0, count);
        }
    }
}