using Microsoft.Extensions.Logging;

namespace MauiAppCoach
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // --- GESTION DE LA BASE DE DONNÉES ---

            // 1. Définir le chemin du fichier (propre à Android/iOS/Windows)
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "dbcoach.db3");

            // 2. Enregistrer SQLiteDb en tant que Singleton
            // Singleton signifie qu'une seule instance sera créée pour toute la durée de vie de l'app.
            builder.Services.AddSingleton<SQLiteDb>(s => new SQLiteDb(dbPath));

            // 3. Enregistrer tes pages (Important pour que l'injection fonctionne)
            builder.Services.AddTransient<MainPage>();
            // AddTransient crée une nouvelle instance de la page à chaque fois qu'on y va.

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}