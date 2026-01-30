<details>
  <summary><b>📜 Historique des versions (cliquer pour dérouler)</b></summary>

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

  ### v2.0
  - Sérialisation JSON des profils.
  - Gestion persistante des données via `FileSystem.AppDataDirectory`.
  - Ajout de la bibliothèque de classes `CoachLibrairie`.
  - Animations UI (`FadeTo`) et feedback haptique.

  ### v1.0
  - [Version initiale](https://github.com/leoalmy/CoachApp) : Calcul d'IMG de base pour Android.
</details>


Application mobile développée avec **.NET MAUI** pour calculer, analyser et **sauvegarder** l'Indice de Masse Grasse (IMG).

## 🆕 Nouveautés de la Version 2

Cette version apporte des améliorations majeures au niveau de l'architecture et de l'expérience utilisateur :

- **Architecture Découplée** : Migration de la logique métier dans une bibliothèque de classes partagée (`CoachLibrairie`).
- **Persistance des Données** : Sauvegarde et récupération automatique du profil utilisateur via sérialisation **JSON**.
- **UX Dynamique** : 
    - Animations de fondu (`FadeTo`) lors de l'affichage des résultats.
    - Feedback haptique (**Vibrations**) différencié selon les résultats.
- **Refactorisation DRY** : Centralisation de la logique d'affichage pour éliminer la redondance de code.

## 🏗️ Architecture du Projet

Le projet adopte une structure modulaire pour séparer l'interface de la logique :

- **`CoachLibrairie` (Library)** : Contient les classes `Profil` (calculs) et `Serializer` (persistance).
- **`MauiAppCoach` (App UI)** : Interface utilisateur MAUI, gestion des animations et des périphériques (vibration).
- **`Coach.Tests` (Tests xUnit)** : Suite de tests unitaires validant les calculs et la sauvegarde.

### Flux de données
Le diagramme suivant illustre le fonctionnement de la sauvegarde et de la récupération :



## 🛠️ Modèle Métier & Persistance

### Classe `Profil`
Réécrite pour être compatible avec la sérialisation moderne :
- Utilisation de **propriétés auto-implémentées** (`Sexe`, `Poids`, `Taille`, `Age`).
- Constructeur vide par défaut requis par `System.Text.Json`.

### Sérialisation JSON
Les données sont stockées localement dans un fichier `saveprofil.json` situé dans le répertoire privé de l'application (`FileSystem.AppDataDirectory`).

## 🎨 Expérience Utilisateur (UX)

- **Feedback Visuel** : Utilisation de `Task.WhenAll` pour animer l'apparition de l'image et du message de résultat.
- **Feedback Haptique** : 
    - **Résultat Parfait** : Vibration courte (50ms).
    - **Résultat Alerte (Trop maigre / Surpoids)** : Vibration longue (500ms).

## 🧪 Tests Unitaires

La V2 utilise **xUnit** pour garantir la stabilité du code.

| Type | Test | Objectif |
|------|------|----------|
| **Métier** | `Femme_RetourneParfait` | Valide les seuils d'IMG pour les femmes. |
| **Métier** | `Homme_RetourneSurpoids` | Valide les seuils d'IMG pour les hommes. |
| **Données** | `Serialize_Puis_Deserialize` | Vérifie que l'objet récupéré est identique à l'original. |

## 🔧 Installation & Configuration

1. **Permission Android** : Vérifiez la présence de `<uses-permission android:name="android.permission.VIBRATE" />` dans `AndroidManifest.xml`.
2. **Chemin de données** : Le fichier est stocké dans le dossier interne de l'app.
3. **Reset des données** : Pour vider le cache sur Android, allez dans *Paramètres > Applis > MauiAppCoach > Stockage > Effacer les données*.

---
**Développé avec ❤️ en .NET 9.0**
