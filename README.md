<details>
  <summary><b>📜 Historique des versions (cliquer pour dérouler)</b></summary>

  ### v5.0 (Dernière version)
  - **Base de données distante** : Migration de SQLite local vers une API REST cloud.
  - **Communication HTTP/REST** : Nouvelle classe `AccesDistant` pour la synchronisation avec le serveur.
  - **Synchronisation multi-appareils** : Accès aux profils depuis n'importe quel appareil via le cloud.
  - **Sérialisation JSON** : Échange de données structuré avec le serveur via JSON.
  - **Endpoints API sécurisés** : Communication DDNS avec un serveur backend dédié.

  ### v4.0
  - **Navigation multi-page** avec AppShell pour une meilleure expérience utilisateur.
  - **Injection de dépendances** (DI) pour une architecture plus robuste et testable.
  - **Page d'historique** : Consultation et visualisation des profils avec tri chronologique.
  - **Architecture par couches** : Séparation claire entre UI (MAUI), logique métier et persistance.
  - **Gestion centralisée de la base de données** : SQLiteDb enregistré en Singleton pour un accès unifié.
  - Disponible à cette adresse -> [v4.0](https://pepepc.ddns.net/home/index.php?page=projet_detail&projet=coachapp-v4)

  ### v3.0
  - **Passage de JSON à SQLite** pour une gestion robuste des données.
  - **Historique complet** : Sauvegarde et consultation de tous les profils mesurés.
  - **Modèle d'accès amélioré** : Classe `SQLiteDb` avec opérations CRUD asynchrones.
  - **Gestion d'ID** : Intégration de clés primaires auto-incrémentées.
  - **Tests asynchrones** : Suite de tests SQLite avec base de données en mémoire.
  - Disponible à cette adresse -> [v3.0](https://pepepc.ddns.net/home/index.php?page=projet_detail&projet=coachapp-v3)

  ### v2.0
  - Sérialisation JSON des profils.
  - Gestion persistante des données via `FileSystem.AppDataDirectory`.
  - Ajout de la bibliothèque de classes `CoachLibrairie`.
  - Animations UI (`FadeTo`) et feedback haptique.
  - Disponible à cette adresse -> [v2.0](https://pepepc.ddns.net/home/index.php?page=projet_detail&projet=coachapp-v2)

  ### v1.0
  - Calcul d'IMG de base pour Android.
  - Disponible à cette adresse -> [v1.0](https://pepepc.ddns.net/home/index.php?page=projet_detail&projet=coachapp)
</details>

---

Application mobile développée avec **.NET MAUI** pour calculer, analyser et **synchroniser** l'Indice de Masse Grasse (IMG) via une base de données cloud.

---

## 🆕 Nouveautés de la Version 5

Cette version introduit une **séparation client-serveur avec une communication API HTTP** et une **base de données centralisée** :

- **Base de Données Centralisée** : Remplacement de SQLite local par une base de données accessible via pages PHP (locale ou distante).
- **Communication HTTP/REST** : Nouvelle classe `AccesDistant` pour gérer les requêtes POST/GET vers les pages PHP.
- **Pages PHP Simples** : Scripts de communication basiques (`insertprofil.php`, `selectprofil.php`, `selecthistorique.php`).
- **Synchronisation Multi-Appareils** : Tous les profils sont centralisés, permettant un accès depuis plusieurs appareils.
- **Sérialisation JSON** : Utilisation de `JsonSerializer` pour la sérialisation/désérialisation des données avec le serveur.
- **Flexibilité d'Hébergement** : Le serveur PHP peut être local (XAMPP) ou distant (DDNS).

## 🏗️ Architecture du Projet

Le projet adopte une **architecture client-serveur avec communication HTTP** :

- **`CoachLibrairie` (Library)** : Contient les classes `Profil` (calculs) et `AccesDistant` (communication HTTP avec les pages PHP).
- **`MauiAppCoach` (App UI)** : Interface utilisateur MAUI multi-page avec `AppShell` pour la navigation, utilise `AccesDistant` pour accéder aux données.
- **`CoachTests` (Tests xUnit)** : Suite de tests unitaires validant les calculs.
- **Pages PHP** : Scripts simples hébergés sur un serveur PHP (local ou distant) pour gérer les opérations de base de données.

### Navigation Multi-Page

L'application utilise **AppShell** pour gérer la navigation entre les pages :

- **MenuPage** : Point d'entrée avec deux boutons de navigation (Calculer, Historique).
- **MainPage** : Page de calcul avec saisie des données, envoi vers le serveur distant et affichage du résultat.
- **HistoPage** : Page d'historique récupérant et affichant tous les profils triés du plus récent au plus ancien.

### Communication avec le Serveur PHP

La classe `AccesDistant` gère toute la communication HTTP avec un serveur PHP. Ce serveur peut être :
- **Local** : Une instance XAMPP en local (`http://localhost/coachapp-db/`)
- **Distant** : Un serveur avec un DDNS (`https://pepepc.ddns.net/coachapp-db/`)

```csharp
// Instance créée dans les pages pour accéder à la base de données
var accesDistant = new AccesDistant();

// Insertion d'un nouveau profil
await accesDistant.AjoutProfil(leProfil);

// Récupération du dernier profil
var dernierProfil = await accesDistant.RecupDernierProfil();

// Récupération de l'historique complet
var tousLesProfils = await accesDistant.RecupTousLesProfils();
```

**Pages PHP simples utilisées** :
- `{SERVER_URL}/insertprofil.php` (POST) : Insère un nouveau profil dans la base de données
- `{SERVER_URL}/selectprofil.php` (GET) : Récupère le dernier profil enregistré
- `{SERVER_URL}/selecthistorique.php` (GET) : Récupère l'historique complet des profils

L'URL de base du serveur (`{SERVER_URL}`) est configurable dans la classe `AccesDistant` pour permettre le basculement entre environnements :
- **Local** : `http://localhost/coachapp-db/`
- **Distant** : URL d'un serveur DDNS (exemple : `https://pepepc.ddns.net/coachapp-db/`)

**Note** : Ces pages sont privées et accessibles uniquement avec les bonnes permissions réseau.

La communication se fait en **JSON**, avec une configuration `JsonSerializerOptions` pour gérer la nomenclature `camelCase` côté PHP.

## 🛠️ Modèle Métier & Communication Distante

### Classe `Profil`
Enrichie pour supporter la sérialisation JSON et la communication avec le serveur distant :
- **Attributs clés** : `Id` (identifiant), `Datemesure` (`DateTimeOffset`), `Sexe`, `Poids`, `Taille`, `Age`, `Img`, `Message`.
- **Constructeurs** : Constructeur paramétré pour créer un profil avec calcul automatique, et constructeur vide requis par JsonSerializer.
- **Décorateurs JSON** : `[JsonPropertyName("...")]` sur les propriétés pour mapper les noms JSON avec les propriétés C#.

### Classe `AccesDistant`
Gère la communication HTTP avec le serveur PHP de manière asynchrone :
- **`AjoutProfil(Profil)`** : Envoie un nouveau profil au script `insertprofil.php` via POST en JSON.
- **`RecupDernierProfil()`** : Récupère le dernier profil depuis `selectprofil.php` via GET et le désérialise.
- **`RecupTousLesProfils()`** : Récupère la liste complète des profils depuis `selecthistorique.php` via GET et les désérialise.
- **Gestion des erreurs** : Try-catch avec logging en Debug pour tracer les erreurs réseau et les problèmes de communication.
- **Sérialisation JSON** : Configuration avec `JsonSerializerOptions` pour utiliser la nomenclature `camelCase` côté serveur.

### Stockage des données
Les données sont stockées dans une base de données distante gérée par les scripts PHP et accessibles via HTTP REST. L'absence de stockage local signifie que l'application dépend de la connectivité réseau pour fonctionner.

## 🎯 Avantages de la V5 par rapport à la V4

| Aspect | V4 | V5 |
|--------|----|----|
| **Persistance des données** | SQLite local | Base de données centralisée (pages PHP) |
| **Communication** | Accès direct à une base de données locale | Communication HTTP avec pages PHP |
| **Synchronisation** | Données isolées par appareil | Données centralisées, accessibles depuis plusieurs appareils |
| **Scalabilité** | Limité à la capacité du stockage local | Scalabilité du serveur PHP |
| **Accès offline** | Possible avec données locales | Nécessite une connexion réseau |
| **Hébergement** | N/A | Flexible : XAMPP local ou serveur DDNS distant |
| **Sauvegarde centralisée** | Responsabilité de l'utilisateur | Gérée par le serveur PHP |

## 📱 Pages et Composants

### MenuPage
Page d'accueil avec deux actions principales :
- **Bouton "Calculer"** : Navigation vers `MainPage` pour calculer l'IMG.
- **Bouton "Historique"** : Navigation vers `HistoPage` pour consulter l'historique complet.

### MainPage
Page de saisie et calcul avec synchronisation cloud :
- **Saisie des données** : Poids, Taille, Âge, Sexe (radio buttons Homme/Femme).
- **Calcul automatique** : Création d'un objet `Profil` avec calcul d'IMG et génération du message.
- **Sauvegarde asynchrone** : Insertion dans la base de données distante via `accesDistant.AjoutProfil()`.
- **Affichage du résultat** : Animations en fade-in + feedback haptique adapté.
- **Récupération du dernier profil** : Au chargement de la page, affichage du dernier profil enregistré.
- **Utilisation d'`AccesDistant`** : Instanciation et utilisation directe de la classe pour la communication HTTP.

### HistoPage
Page de consultation de l'historique cloud :
- **Chargement au démarrage** : `OnAppearing()` récupère tous les profils via `RecupTousLesProfils()`.
- **Tri décroissant** : Profils triés du plus récent au plus ancien (`OrderByDescending` sur `Datemesure`).
- **Data Binding** : Liaison avec XAML via `BindingContext` anonyme contenant `ListeProfils`.

## 🎨 Expérience Utilisateur (UX)

- **Navigation Fluide** : AppShell offre une navigation cohérente entre les pages avec retour arrière automatique.
- **Feedback Visuel** : Utilisation de `Task.WhenAll` pour animer l'apparition de l'image et du message de résultat.
- **Feedback Haptique Détaillé** :
    - **Résultat Parfait** : Vibration courte (1500ms).
    - **Résultat Alerte (Trop maigre / Surpoids)** : Deux vibrations longues (1001ms chacune) avec pause.
- **Expérience Responsive** : Opérations asynchrones empêchent les blocages UI pendant les accès à la base de données distante.

## 🧪 Tests Unitaires

La V5 utilise **xUnit** pour garantir la stabilité des calculs métier.

| Type | Test | Objectif |
|------|------|----------|
| **Métier** | `Femme_RetourneParfait` | Valide les seuils d'IMG pour les femmes. |
| **Métier** | `Homme_RetourneSurpoids` | Valide les seuils d'IMG pour les hommes. |
| **Métier** | `CalculIMG_CorrectemeFemmeNormal` | Vérifie le calcul IMG pour une femme avec valeurs normales |
| **Métier** | `CalculIMG_CorrectementHommeNormal` | Vérifie le calcul IMG pour un homme avec valeurs normales |

## 🔧 Installation & Configuration (V5)

1. **Dépendances NuGet** : Les packages requis sont listés dans `MauiAppCoach.csproj`.
2. **Permission Android** : Vérifiez la présence de `<uses-permission android:name="android.permission.VIBRATE" />` dans `AndroidManifest.xml`.
3. **Connectivité réseau** : L'application requiert une connexion Internet active pour fonctionner.
4. **Configuration du serveur** : Deux options possibles :
   - **Local** : Utiliser XAMPP avec la base de données locale et modifier l'URL dans `AccesDistant` vers `http://localhost/coachapp-db/`
   - **Distant** : Utiliser un serveur DDNS comme `https://pepepc.ddns.net/coachapp-db/` (URL par défaut)
5. **Navigation Shell** : L'application utilise `AppShell.xaml` pour configurer les routes de navigation.

## 📦 Dépendances Principales

```xml
<PackageReference Include="Microsoft.Maui.Controls" Version="9.0.0" />
<PackageReference Include="xunit" Version="2.x" />
```

### Technologie côté client :
- **HTTP Client** : Utilisation native de `HttpClient` pour les requêtes HTTP.
- **JSON Serialization** : `System.Text.Json` pour la sérialisation/désérialisation.

### Technologie côté serveur :
- **PHP** : Langage pour les pages simples de gestion de la base de données.
- **Base de données** : Peut être MySQL, MariaDB ou autre (dépend du serveur PHP).

---
**Développé avec ❤️ en .NET 9.0 + MAUI + PHP**
