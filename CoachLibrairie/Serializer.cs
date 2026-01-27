using System.Text.Json;
using CoachLibrairie;

public abstract class Serializer
{
    // On demande le 'dossier' en paramètre pour être flexible
    public static void Serialize(string dossier, string nomFichier, Profil profil)
    {
        string fichier = Path.Combine(dossier, nomFichier);

        try
        {
            string jsonString = JsonSerializer.Serialize(profil);
            File.WriteAllText(fichier, jsonString);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur sérialisation : {ex.Message}");
        }
    }

    public static Profil Deserialize(string dossier, string nomFichier)
    {
        string fichier = Path.Combine(dossier, nomFichier);

        if (File.Exists(fichier))
        {
            try
            {
                string jsonString = File.ReadAllText(fichier);
                return JsonSerializer.Deserialize<Profil>(jsonString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur désérialisation : {ex.Message}");
            }
        }
        return null;
    }
}