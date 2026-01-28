using SQLite;
using System;

[Serializable]
public class Profil
{
    // --- Attributs privés ---
    private DateTimeOffset datemesure;
    private int sexe;
    private int poids;
    private int taille;
    private int age;
    private double img;
    private string message;

    // --- Constructeurs ---

    public Profil(int? unId, DateTimeOffset uneDate, int unPoids, int uneTaille, int unAge, int unSexe)
    {
        // On passe par les propriétés pour déclencher les logiques si besoin
        this.Id = unId ?? 0; // Gestion du null pour la BDD
        this.Datemesure = uneDate;
        this.Poids = unPoids;
        this.Taille = uneTaille;
        this.Age = unAge;
        this.Sexe = unSexe;

        CalculIMG();
        ResultatIMG();
    }

    public Profil()
    {
        this.datemesure = DateTimeOffset.Now;
        this.sexe = 0;
        this.poids = 0;
        this.taille = 0;
        this.age = 0;
        this.img = 0;
        this.message = "";
    }

    // --- Propriétés (Getters / Setters) ---

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTimeOffset Datemesure
    {
        get => datemesure;
        set => datemesure = value;
    }

    public int Sexe
    {
        get => sexe;
        set => sexe = value;
    }

    public int Poids
    {
        get => poids;
        set => poids = value;
    }

    public int Taille
    {
        get => taille;
        set => taille = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public double Img
    {
        get => img;
        set => img = value;
    }

    public string Message
    {
        get => message;
        set => message = value;
    }

    // --- Méthodes de calcul ---

    private void CalculIMG()
    {
        if (this.Taille == 0) return; // Évite la division par zéro

        // Conversion de la taille en mètres
        double tailleMetre = (double)this.Taille / 100;

        // Formule de Deurenberg : (1.2 * IMC) + (0.23 * Age) - (10.83 * Sexe) - 5.4
        // IMC = Poids / (Taille en m)²
        this.Img = (1.2 * this.Poids / (tailleMetre * tailleMetre)) + (0.23 * this.Age) - (10.83 * this.Sexe) - 5.4;
    }

    private void ResultatIMG()
    {
        if (this.Sexe == 0) // Femmes
        {
            if (this.Img < 25) this.Message = "Trop maigre.";
            else if (this.Img <= 30) this.Message = "Parfait.";
            else this.Message = "Surpoids.";
        }
        else // Hommes
        {
            if (this.Img < 15) this.Message = "Trop maigre.";
            else if (this.Img <= 20) this.Message = "Parfait.";
            else this.Message = "Surpoids.";
        }
    }
}