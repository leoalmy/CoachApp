<details>
  <summary><b>📜 Historique des versions (cliquer pour dérouler)</b></summary>

  ### v3.0
  - **Passage de JSON à SQLite** pour une gestion robuste des données.
  - **Historique complet** : Sauvegarde et consultation de tous les profils mesurés.
  - **Modèle d'accès amélioré** : Classe `SQLiteDb` avec opérations CRUD asynchrones.
  - **Gestion d'ID** : Intégration de clés primaires auto-incrémentées.
  - **Tests asynchrones** : Suite de tests SQLite avec base de données en mémoire.

  ### v2.0
  - Sérialisation JSON des profils.
  - Gestion persistante des données via `FileSystem.AppDataDirectory`.
  - Ajout de la bibliothèque de classes `CoachLibrairie`.
  - Animations UI (`FadeTo`) et feedback haptique.

  ### v1.0
  - [Version initiale](https://github.com/leoalmy/CoachApp) : Calcul d'IMG de base pour Android.
</details>


Application mobile développée avec **.NET MAUI** pour calculer, analyser et **sauvegarder** l'Indice de Masse Grasse (IMG).

## 🆕 Nouveautés de la Version 3

Cette version marque un tournant majeur avec l'introduction d'une **base de données SQLite** pour remplacer la persistance JSON :

- **Base de Données SQLite** : Stockage fiable et structuré des profils via `sqlite-net-pcl`.
- **Historique Complet** : Accès à tous les profils enregistrés (dernier, tous les profils, suppression).
- **Architecture CRUD** : Classe `SQLiteDb` avec méthodes asynchrones (`SaveProfilAsync`, `GetLastProfilAsync`, `GetAllProfilsAsync`, `DeleteProfilAsync`).
- **Gestion des Identifiants** : Intégration de clés primaires auto-incrémentées pour chaque profil.
- **Tests Robustes** : Suite de tests unitaires avec base de données en mémoire (`:memory:`), évitant les effets de bord.
- **Classe Profil Enrichie** : Ajout de l'attribut `DateTimeOffset` pour tracer la date de chaque mesure.

## 🏗️ Architecture du Projet

Le projet adopte une structure modulaire pour séparer l'interface de la logique :

- **`CoachLibrairie` (Library)** : Contient les classes `Profil` (calculs) et `SQLiteDb` (accès aux données).
- **`MauiAppCoach` (App UI)** : Interface utilisateur MAUI, gestion des animations, des périphériques (vibration) et interaction avec la base de données.
- **`CoachTests` (Tests xUnit)** : Suite de tests unitaires validant les calculs et les opérations SQLite.

### Architecture de la base de données
La classe `SQLiteDb` gère l'ensemble des opérations sur la base de données avec des méthodes asynchrones :



## 🛠️ Modèle Métier & Persistance

### Classe `Profil`
Enrichie pour supporter la persistance SQLite :
- **Attributs clés** : `Id` (clé primaire auto-incrémentée), `Datemesure` (`DateTimeOffset`), `Sexe`, `Poids`, `Taille`, `Age`, `Img`, `Message`.
- **Constructeurs** : Constructeur paramétré pour créer un profil avec calcul automatique, et constructeur vide requis par SQLite.
- **Décorateurs SQLite** : `[PrimaryKey, AutoIncrement]` sur la propriété `Id`.

### Classe `SQLiteDb`
Gère toutes les opérations CRUD de manière asynchrone :
- **`SaveProfilAsync(Profil)`** : Insère un nouveau profil ou met à jour un profil existant (vérification de l'ID).
- **`GetLastProfilAsync()`** : Récupère le dernier profil enregistré (idéal pour l'affichage immédiat).
- **`GetAllProfilsAsync()`** : Récupère l'historique complet de tous les profils.
- **`DeleteProfilAsync(Profil)`** : Supprime un profil de la base de données.
- **`CloseConnectionAsync()`** : Ferme proprement la connexion à la base.

### Stockage des données
Les données sont stockées dans un fichier `dbcoach.db3` situé dans le répertoire privé de l'application.

## 🎯 Avantages de SQLite par rapport à JSON

| Aspect | JSON (V2) | SQLite (V3) |
|--------|-----------|-----------|
| **Requêtes** | Chargement complet en mémoire | Requêtes SQL optimisées |
| **Scalabilité** | Lent avec beaucoup de données | Performant même avec 10k+ enregistrements |
| **Intégrité des données** | Aucune contrainte | Clés primaires, types fortement typés |
| **Transactions** | Non supportées | Transactions ACID complètes |
| **Historique** | Suppression d'ancien fichier | Gestion complète de l'historique |

## 🎨 Expérience Utilisateur (UX)

- **Feedback Visuel** : Utilisation de `Task.WhenAll` pour animer l'apparition de l'image et du message de résultat.
- **Feedback Haptique** : 
    - **Résultat Parfait** : Vibration courte (50ms).
    - **Résultat Alerte (Trop maigre / Surpoids)** : Vibration longue (500ms).

## 🧪 Tests Unitaires

La V3 utilise **xUnit** avec des bases de données en mémoire pour garantir l'isolation des tests.

| Type | Test | Objectif |
|------|------|----------|
| **CRUD** | `SaveProfilAsync_NouveauProfil_InsertionReussie` | Valide l'insertion d'un nouveau profil. |
| **CRUD** | `SaveProfilAsync_ProfilExistant_MiseAJourReussie` | Valide la mise à jour d'un profil existant. |
| **Lecture** | `GetLastProfilAsync_RetourneDernierProfil` | Vérifie la récupération du dernier profil. |
| **Lecture** | `GetAllProfilsAsync_RetourneHistorique` | Valide l'accès à l'historique complet. |
| **Suppression** | `DeleteProfilAsync_SupprimeProfil` | Vérifie la suppression d'un profil. |
| **Métier** | `Femme_RetourneParfait` | Valide les seuils d'IMG pour les femmes. |
| **Métier** | `Homme_RetourneSurpoids` | Valide les seuils d'IMG pour les hommes. |

## 🔧 Installation & Configuration

1. **Dépendance NuGet** : Le package `sqlite-net-pcl` (v1.9.172+) est requis dans `MauiAppCoach.csproj`.
2. **Permission Android** : Vérifiez la présence de `<uses-permission android:name="android.permission.VIBRATE" />` dans `AndroidManifest.xml`.
3. **Chemin de la base de données** : Le fichier `dbcoach.db3` est stocké dans le dossier privé de l'application.
4. **Reset des données** : Pour supprimer la base de données sur Android, allez dans *Paramètres > Applis > MauiAppCoach > Stockage > Effacer les données*.

## 📦 Dépendances de V3

```xml
<PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
```

---
**Développé avec ❤️ en .NET 9.0 + SQLite**
