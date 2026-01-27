<details>
  <summary><b>📜 Historique des versions (cliquer pour dérouler)</b></summary>

  ### v2.0 (Dernière version)
  - Sérialisation JSON et persistance.
  - Architecture en bibliothèque de classes.
  - Animations et vibrations.
  - Disponible sur la branche : [v2-serialisation-json](https://github.com/leoalmy/CoachApp/tree/v2-serialisation-json)

  ### v1.0 (Version actuelle)
  - Calcul d'IMG de base.
  - Interface utilisateur initiale.
  - Logique métier intégrée au projet principal.
</details>

Une application mobile multi-plateforme développée avec **.NET MAUI** pour calculer et analyser l'Indice de Masse Grasse (IMG) des utilisateurs.

## 🎯 Objectif

CoachAppV1 est une application de coaching personnel qui permet aux utilisateurs de :
- **Calculer leur Indice de Masse Grasse (IMG)** en fonction de paramètres personnels
- **Analyser leurs résultats** avec des commentaires adaptés
- **Suivre leur profil** basé sur le poids, la taille, l'âge et le sexe

## 🚀 Fonctionnalités

- ✅ Calcul automatique de l'IMG selon la formule scientifique
- ✅ Analyse personnalisée basée sur le profil (sexe, âge)
- ✅ Interface intuitive et réactive
- ✅ Gestion des erreurs de saisie avec messages utilisateur
- ✅ Messages de feedback détaillés selon les résultats
- ✅ Affichage d'images emoji adaptées au résultat (Trop maigre, Parfait, Surpoids)

## 📋 Configuration Système

- **.NET Framework** : .NET 9.0
- **Framework UI** : Microsoft.NET.Sdk + MAUI
- **Version Application** : 1.0
- **ID Application** : com.SIO.MauiAppCoachV1
- **Namespace Principal** : `testSIO` / `MauiAppCoachV1`
- **Plateforme Cible** : Android uniquement

### Plateforme Android

| Plateforme | Framework Target | Version Minimale |
|------------|------------------|------------------|
| Android    | net9.0-android   | 21.0 (API 50+)   |

## 📦 Dépendances

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.Maui.Controls" />
    <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="9.0.8" />
</ItemGroup>
```

## 🏗️ Architecture du Projet

### Structure des Fichiers

```
MauiAppCoachV1/
├── App.xaml / App.xaml.cs          # Configuration globale de l'application
├── AppShell.xaml / AppShell.xaml.cs # Navigation shell
├── MainPage.xaml / MainPage.xaml.cs # Interface principale
├── MauiProgram.cs                   # Configuration MAUI
├── GlobalXmlns.cs                   # Namespaces globaux
├── Modele/
│   └── Profil.cs                    # Classe métier pour les calculs IMG
├── Platforms/                       # Code spécifique à chaque plateforme
│   └── Android/
├── Resources/                       # Ressources (icônes, fonts, images)
│   ├── AppIcon/
│   ├── Fonts/
│   ├── Images/
│   │   ├── smiley_maigre
│   │   ├── smiley_parfait
│   │   └── smiley_surpoids
│   ├── Splash/
│   └── Styles/
└── Properties/
    └── launchSettings.json
```

### Modèle Métier

#### **Classe `Profil` (namespace `testSIO.Modele`)**

La classe `Profil` gère tous les calculs d'IMG :
- **Paramètres** : sexe (0=Femme, 1=Homme), poids (kg), taille (cm), âge (années)
- **Calcul automatique** : Le calcul et l'interprétation s'effectuent lors de l'instanciation
- **Analyse** : Génération de messages de feedback basés sur le résultat et le sexe

**Formule appliquée** :
```
IMG = (1.2 × Poids / Taille²) + (0.23 × Âge) - (10.83 × Sexe) - 5.4
```

**Seuils d'interprétation** :
- **Femmes** : Trop maigre (<25), Parfait (25-30), Surpoids (>30)
- **Hommes** : Trop maigre (<15), Parfait (15-20), Surpoids (>20)

**Méthodes publiques** :
public float GetImg()      // Retourne l'IMG calculé
public string GetMessage() // Retourne le message d'interprétation

## 🎨 Interface Utilisateur

### MainPage (Page Principale)

L'interface permet de saisir et calculer l'IMG :

- **Champs de saisie** :
  - Poids (en kg, type float)
  - Taille (en cm, type int)
  - Âge (en années, type int)
- **Sélection du sexe** : RadioButton (Homme/Femme)
- **Bouton "Calculer"** : Déclenche le calcul et affiche les résultats
- **Affichage des résultats** :
  - Valeur numérique de l'IMG avec 2 décimales
  - Message personnalisé selon le profil
  - Image emoji correspondante (smiley_maigre, smiley_parfait, smiley_surpoids)
- **Gestion des erreurs** : Alerte utilisateur si les valeurs saisies sont invalides

## 🔧 Installation et Configuration

### Prérequis
- .NET 9.0 SDK
- Visual Studio 2022 (v17.8+) ou Visual Studio Code avec l'extension C#
- Android SDK (API 50+)

### Étapes d'Installation

1. **Cloner le projet**
   ```bash
   git clone https://github.com/leoalmy/CoachAppV1
   cd MauiAppCoachV1
   ```

2. **Restaurer les dépendances**
   ```bash
   dotnet restore
   ```

3. **Compiler le projet**
   ```bash
   dotnet build
   ```

4. **Exécuter l'application sur Android**
   ```bash
   dotnet run -f net9.0-android
   ```

## 📱 Utilisation

1. Lancez l'application
2. Remplissez vos informations personnelles :
- Poids (en kg) - nombre décimal accepté
- Taille (en cm) - nombre entier
- Âge (en années) - nombre entier
- Sexe : Sélectionnez "Femme" ou "Homme"
3. Cliquez sur le bouton "Calculer"
4. Consultez votre IMG et le message correspondant avec l'image emoji

### Exemple de Résultat
Votre IMG : 28.45% - Parfait.
[Affichage de smiley_parfait.png]

## 🛠️ Développement

### Structure du Code

- **App.xaml.cs** : Point d'entrée de l'application
- **MainPage.xaml.cs** : Gestion des événements et interface utilisateur
- **Profil.cs** : Logique métier pour les calculs d'IMG
- **Styles/Styles.xaml** : Thème visuel de l'application
- **Styles/Colors.xaml** : Palette de couleurs

### Namespace du Projet
namespace testSIO.Modele        // Classe métier
namespace MauiAppCoachV1         // Interface utilisateur

### Ajouter une Nouvelle Fonctionnalité

1. Modifiez les fichiers `.xaml` pour l'interface
2. Complétez le code-behind (`.xaml.cs`) pour la logique
3. Mettez à jour la classe `Profil` si nécessaire pour les calculs
4. Testez sur Android

## 🧪 Tests Unitaires

Le projet inclut une **suite complète de 14 tests unitaires** couvrant la classe `Profil` avec une excellente couverture de code.

### 📂 Localisation
- **Dossier** : `MauiAppCoachV1.Tests/`
- **Fichier principal** : `Modele/ProfilTests.cs`
- **Framework** : MSTest
- **Plateforme cible** : .NET 9.0

### 🧬 Tests Couverts

| # | Test | Description |
|----|------|-------------|
| 1 | `CalculeIMG_CorrectemeFemmeNormal` | Vérifie le calcul IMG pour une femme avec valeurs normales | 
| 2 | `CalculeIMG_CorrectementHommeNormal` | Vérifie le calcul IMG pour un homme avec valeurs normales |
| 3 | `InterpretationIMG_FemmeTropMaigre` | Validation du message "Trop maigre" pour une femme |
| 4 | `InterpretationIMG_FemmeSurpoids` | Validation du message "Surpoids" pour une femme |
| 5 | `InterpretationIMG_HommeTropMaigre` | Validation du message "Trop maigre" pour un homme |
| 6 | `InterpretationIMG_HommeSurpoids` | Validation du message "Surpoids" pour un homme |
| 7 | `CalculIMG_FemmeLimiteInferieureParlait` | Test aux limites inférieures pour femme |
| 8 | `CalculIMG_HommeLimiteInferieureParlait` | Test aux limites inférieures pour homme |
| 9 | `CalculIMG_ValeursTresJeune` | Test avec valeurs extrêmes (jeune âge) |
| 10 | `CalculIMG_ValeursAgeAvance` | Test avec valeurs extrêmes (âge avancé) |
| 11 | `GetImg_RetourneValeurPositive` | Validation que l'IMG est toujours positive |
| 12 | `GetMessage_RetourneStringNonNull` | Validation que le message n'est jamais null |
| 13 | `GetMessage_RetourneMessageValide` | Validation que le message est valide |
| 14 | `CalculIMG_ComparaisonSexe` | Vérification des différences entre sexes |

### 🚀 Exécuter les Tests

**Exécuter tous les tests** :
```bash
dotnet test MauiAppCoachV1.Tests
```

**Exécuter avec sortie détaillée** :
```bash
dotnet test MauiAppCoachV1.Tests --verbosity=detailed
```

**Exécuter avec filtrage par nom complet** :
```bash
dotnet test MauiAppCoachV1.Tests --filter "FullyQualifiedName~CalculeIMG_CorrectemeFemmeNormal"
```

**Générer un rapport de couverture de code** :
```bash
dotnet test MauiAppCoachV1.Tests /p:CollectCoverage=true /p:CoverageFormat=cobertura
```

### 📊 Couverture de Code

La suite de tests couvre :
- ✅ Tous les chemins de code de la classe `Profil`
- ✅ Les cas normaux et cas limites
- ✅ Les variations par sexe
- ✅ Les valeurs extrêmes (jeune âge, âge avancé)
- ✅ La validation des sorties (IMG positive, message valide)

## 📄 Licence

Ce projet est sous licence **MIT**. Consultez le fichier [LICENSE](LICENSE) pour plus de détails.

### Termes de la Licence MIT

Vous êtes libre de :
- ✅ Utiliser le code à titre commercial ou personnel
- ✅ Modifier le code
- ✅ Distribuer le code
- ✅ Utiliser le code en privé

Avec la condition que :
- ⚠️ La licence et l'avis de copyright doivent être inclus dans toute copie ou portion substantielle du code

## 🤝 Contribution

Les contributions sont bienvenues ! Pour contribuer :
1. Fork le projet
2. Créez une branche (`git checkout -b feature/AmazingFeature`)
3. Committez vos changements (`git commit -m 'Add AmazingFeature'`)
4. Pushez vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrez une Pull Request

## 📧 Support

Pour toute question ou problème, veuillez ouvrir une issue dans le référentiel.

---

**Développé avec ❤️ en .NET MAUI pour Android**
