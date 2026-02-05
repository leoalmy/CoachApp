<details>
  <summary><b>📜 Historique des versions (cliquer pour dérouler)</b></summary>

  ### v5.0 (Dernière version)
  - **Base de données distante** : Migration de SQLite local vers une API REST cloud.
  - **Communication HTTP/REST** : Nouvelle classe `AccesDistant` pour la synchronisation avec le serveur.
  - **Synchronisation multi-appareils** : Accès aux profils depuis n'importe quel appareil via le cloud.
  - **Sérialisation JSON** : Échange de données structuré avec le serveur via JSON.
  - **Endpoints API sécurisés** : Communication DDNS avec un serveur backend dédié.
  - Disponible à cette adresse -> [v5.0](https://github.com/leoalmy/CoachApp/tree/v5-distant-database)

  ### v4.0
  - **Navigation multi-page** avec AppShell pour une meilleure expérience utilisateur.
  - **Injection de dépendances** (DI) pour une architecture plus robuste et testable.
  - **Page d'historique** : Consultation et visualisation des profils avec tri chronologique.
  - **Architecture par couches** : Séparation claire entre UI (MAUI), logique métier et persistance.
  - **Gestion centralisée de la base de données** : SQLiteDb enregistré en Singleton pour un accès unifié.

  ### v3.0
  - **Passage de JSON à SQLite** pour une gestion robuste des données.
  - **Historique complet** : Sauvegarde et consultation de tous les profils mesurés.
  - **Modèle d'accès amélioré** : Classe `SQLiteDb` avec opérations CRUD asynchrones.
  - **Gestion d'ID** : Intégration de clés primaires auto-incrémentées.
  - **Tests asynchrones** : Suite de tests SQLite avec base de données en mémoire.
  - Disponible à cette adresse -> [v3.0](https://github.com/leoalmy/CoachApp/tree/v3-database-sqlite)

  ### v2.0
  - Sérialisation JSON des profils.
  - Gestion persistante des données via `FileSystem.AppDataDirectory`.
  - Ajout de la bibliothèque de classes `CoachLibrairie`.
  - Animations UI (`FadeTo`) et feedback haptique.
  - Disponible à cette adresse -> [v2.0](https://github.com/leoalmy/CoachApp/tree/v2-serialisation-json)

  ### v1.0
  - Calcul d'IMG de base pour Android.
  - Disponible à cette adresse -> [v1.0](https://github.com/leoalmy/CoachApp)
</details>

Application mobile développée avec **.NET MAUI** pour calculer, analyser et **sauvegarder** l'Indice de Masse Grasse (IMG).

## 🆕 Nouveautés de la Version 4

Cette version introduit une **architecture multi-page avec navigation intuitive** et une **injection de dépendances complète** :

- **Navigation par Shell** : Interface multi-page (`MainPage`, `HistoPage`, `MenuPage`) avec AppShell pour une navigation fluide.
- **Injection de Dépendances (DI)** : Enregistrement centralisé de `SQLiteDb` en Singleton via `MauiProgram`.
- **Page d'Historique Complète** : Affichage des profils triés du plus récent au plus ancien avec tri par `DateTimeOffset`.
- **Architecture Découplée** : Pages acceptant `SQLiteDb` par injection de constructeur, sans dépendances globales.
- **Gestion Optimisée de la Base de Données** : Initialisation lazy de la connexion, fermeture propre et cycle de vie maîtrisé.
- **Feedback Haptique Amélioré** : Séquences de vibration distincts pour succès (courte) et alertes (deux longues vibrations).

## 🏗️ Architecture du Projet

Le projet adopte une **architecture modulaire avec injection de dépendances** pour une meilleure séparation des responsabilités :

- **`CoachLibrairie` (Library)** : Contient les classes `Profil` (calculs) et `SQLiteDb` (accès aux données).
- **`MauiAppCoach` (App UI)** : Interface utilisateur MAUI multi-page avec `AppShell` pour la navigation, gestion des animations, feedback haptique et communication avec la base de données via injection.
- **`CoachTests` (Tests xUnit)** : Suite de tests unitaires validant les calculs et les opérations SQLite.

### Navigation Multi-Page

L'application utilise **AppShell** pour gérer la navigation entre les pages :

- **MenuPage** : Point d'entrée avec deux boutons de navigation (Calculer, Historique).
- **MainPage** : Page de calcul avec saisie des données et affichage du résultat.
- **HistoPage** : Page d'historique affichant tous les profils triés du plus récent au plus ancien.

Chaque page reçoit l'instance `SQLiteDb` via le constructeur (injection de dépendances).

### Injection de Dépendances (DI)

Le fichier `MauiProgram.cs` configure le conteneur DI :

```csharp
// Enregistrement de SQLiteDb en Singleton
builder.Services.AddSingleton<SQLiteDb>(s => new SQLiteDb(dbPath));

// Enregistrement des pages en Transient (nouvelle instance à chaque navigation)
builder.Services.AddTransient<MainPage>();
builder.Services.AddTransient<HistoPage>();
builder.Services.AddTransient<MenuPage>();
```

**Avantages** :
- **Une seule instance** de `SQLiteDb` pour toute l'application (Singleton).
- **Dépendances explicites** : Les pages déclarent ce dont elles ont besoin dans leur constructeur.
- **Testabilité** : Possibilité d'injecter une fausse implémentation pour les tests.
- **Pas de dépendances globales** : Pas de static, pas de service locator.

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

## 🎯 Avantages de la V4 par rapport à la V3

| Aspect | V3 | V4 |
|--------|----|----|
| **Navigation** | Single-page monolithique | Multi-page avec AppShell |
| **Gestion des dépendances** | Variables globales/statiques | Injection de dépendances (DI) via MauiProgram |
| **Affichage de l'historique** | Aucun accès direct | Page `HistoPage` dédiée avec tri chronologique |
| **Architecture UI** | Couplée à la logique métier | Découplée via injection de constructeur |
| **Testabilité** | Dépendances difficiles à mocker | Injection facilite les tests avec fausses implémentations |
| **Cycle de vie SQLiteDb** | Gestion manuelle possible | Centralisé en Singleton maîtrisé |
| **Maintenabilité** | Difficile à étendre | Facile d'ajouter de nouvelles pages |

## 🎯 Avantages de SQLite par rapport à JSON

| Aspect | JSON (V2) | SQLite (V3+) |
|--------|-----------|-----------|
| **Requêtes** | Chargement complet en mémoire | Requêtes SQL optimisées |
| **Scalabilité** | Lent avec beaucoup de données | Performant même avec 10k+ enregistrements |
| **Intégrité des données** | Aucune contrainte | Clés primaires, types fortement typés |
| **Transactions** | Non supportées | Transactions ACID complètes |
| **Historique** | Suppression d'ancien fichier | Gestion complète de l'historique |

## 📱 Pages et Composants

### MenuPage
Page d'accueil avec deux actions principales :
- **Bouton "Calculer"** : Navigation vers `MainPage` pour calculer l'IMG.
- **Bouton "Historique"** : Navigation vers `HistoPage` pour consulter l'historique complet.

### MainPage
Page de saisie et calcul :
- **Saisie des données** : Poids, Taille, Âge, Sexe (radio buttons Homme/Femme).
- **Calcul automatique** : Création d'un objet `Profil` avec calcul d'IMG et génération du message.
- **Sauvegarde asynchrone** : Insertion dans la base de données via `_sqliteDbCoach.SaveProfilAsync()`.
- **Affichage du résultat** : Animations en fade-in + feedback haptique adapté.
- **Injection de dépendances** : Reçoit `SQLiteDb` en tant que paramètre de constructeur.

### HistoPage
Page de consultation de l'historique :
- **Chargement au démarrage** : `OnAppearing()` récupère tous les profils via `GetAllProfilsAsync()`.
- **Tri décroissant** : Profils triés du plus récent au plus ancien (`OrderByDescending` sur `Datemesure`).
- **Data Binding** : Liaison avec XAML via `BindingContext` anonyme contenant `ListeProfils`.
- **Injection de dépendances** : Accès à la base de données via le constructeur.

## 🎨 Expérience Utilisateur (UX)

- **Navigation Fluide** : AppShell offre une navigation cohérente entre les pages avec retour arrière automatique.
- **Feedback Visuel** : Utilisation de `Task.WhenAll` pour animer l'apparition de l'image et du message de résultat.
- **Feedback Haptique Détaillé** :
    - **Résultat Parfait** : Vibration courte (1500ms).
    - **Résultat Alerte (Trop maigre / Surpoids)** : Deux vibrations longues (1001ms chacune) avec pause.
- **Architecture Découplée** : Pages ne connaissent pas les détails internes de la base de données, tout passe par `SQLiteDb`.
- **Expérience Responsive** : Opérations asynchrones empêchent les blocages UI pendant les accès à la base de données.

## 🧪 Tests Unitaires

La V4 utilise **xUnit** avec des bases de données en mémoire pour garantir l'isolation des tests. Les tests valident à la fois la logique métier et les opérations SQLite.

| Type | Test | Objectif |
|------|------|----------|
| **CRUD** | `SaveProfilAsync_NouveauProfil_InsertionReussie` | Valide l'insertion d'un nouveau profil. |
| **CRUD** | `SaveProfilAsync_ProfilExistant_MiseAJourReussie` | Valide la mise à jour d'un profil existant. |
| **Lecture** | `GetLastProfilAsync_RetourneDernierProfil` | Vérifie la récupération du dernier profil. |
| **Lecture** | `GetAllProfilsAsync_RetourneHistorique` | Valide l'accès à l'historique complet. |
| **Suppression** | `DeleteProfilAsync_SupprimeProfil` | Vérifie la suppression d'un profil. |
| **Métier** | `Femme_RetourneParfait` | Valide les seuils d'IMG pour les femmes. |
| **Métier** | `Homme_RetourneSurpoids` | Valide les seuils d'IMG pour les hommes. |
| **Navigation V4** | `HistoPage_Tri_PlusRecentEnPremier` | Valide le tri chronologique décroissant des profils dans HistoPage. |

## 🔧 Installation & Configuration (V4)

1. **Dépendances NuGet** : Les packages requis sont listés dans `MauiAppCoach.csproj`.
2. **Injection de Dépendances** : Assurer que `MauiProgram.cs` enregistre correctement `SQLiteDb` en Singleton et les pages en Transient.
3. **Permission Android** : Vérifiez la présence de `<uses-permission android:name="android.permission.VIBRATE" />` dans `AndroidManifest.xml`.
4. **Chemin de la base de données** : Le fichier `dbcoach.db3` est stocké dans le dossier privé de l'application (géré par `FileSystem.AppDataDirectory`).
5. **Navigation Shell** : L'application utilise `AppShell.xaml` pour configurer les routes de navigation.
6. **Reset des données** : Pour supprimer la base de données sur Android, allez dans *Paramètres > Applis > MauiAppCoach > Stockage > Effacer les données*.

## 📦 Dépendances Principales

```xml
<PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
<PackageReference Include="Microsoft.Maui.Controls" Version="9.0.0" />
<PackageReference Include="xunit" Version="2.x" />
```

### Améliorations en V4 :
- **Microsoft.Extensions.DependencyInjection** : Intégré nativement dans MAUI pour l'injection de dépendances.
- **Architecture modulaire** : Séparation claire entre UI (MAUI), logique métier (Profil) et persistance (SQLiteDb).

---
**Développé avec ❤️ en .NET 9.0 + SQLite + MAUI Shell**
