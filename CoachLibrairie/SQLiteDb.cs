using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SQLiteDb
{
    public const string DatabaseFilename = "dbcoach.db3";
    private readonly string _databasePath;

    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.Create |
                                         SQLiteOpenFlags.ReadWrite |
                                         SQLiteOpenFlags.SharedCache;

    private SQLiteAsyncConnection connection;

    public SQLiteDb(string dbPath)
    {
        // Si dbPath est null ici, l'app plantera avec la NullReferenceException que tu vois
        _databasePath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
    }

    // Permet de passer une connexion déjà ouverte (pour les tests)
    public SQLiteDb(SQLiteAsyncConnection existingConnection)
    {
        connection = existingConnection;
    }

    private async Task Initialize()
    {
        if (connection is not null)
            return;

        // Utilisation du chemin ET des flags
        connection = new SQLiteAsyncConnection(_databasePath, Flags);

        await connection.CreateTableAsync<Profil>();
    }

    public async Task<int> SaveProfilAsync(Profil unProfil)
    {
        await Initialize();
        // Si l'ID est supérieur à 0, c'est que l'objet existe déjà en BDD
        if (unProfil.Id > 0)
        {
            return await connection.UpdateAsync(unProfil);
        }
        else
        {
            return await connection.InsertAsync(unProfil);
        }
    }

    // Récupère uniquement le dernier enregistrement (pratique pour l'affichage immédiat)
    public async Task<Profil> GetLastProfilAsync()
    {
        await Initialize();
        return await connection.Table<Profil>()
                               .OrderByDescending(p => p.Id)
                               .FirstOrDefaultAsync();
    }

    // Récupère TOUT l'historique
    public async Task<List<Profil>> GetAllProfilsAsync()
    {
        await Initialize();
        return await connection.Table<Profil>().ToListAsync();
    }

    // Supprimer un profil si besoin
    public async Task<int> DeleteProfilAsync(Profil unProfil)
    {
        await Initialize();
        return await connection.DeleteAsync(unProfil);
    }

    // Fermer la connexion à la base de données
    public async Task CloseConnectionAsync()
    {
        if (connection is not null)
        {
            await connection.CloseAsync();
            connection = null; // Important pour forcer la libération
        }
    }
}