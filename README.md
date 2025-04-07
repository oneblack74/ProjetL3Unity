# 🕹️ Unity-L3-GameProject

🎓 Projet de Licence 3 réalisé au second semestre dans le cadre d’un travail de groupe.  
L’objectif était de concevoir un **jeu Unity** en respectant des **contraintes hebdomadaires** données par les enseignants.

---

## 📌 Fonctionnalités demandées

Voici les grandes fonctionnalités implémentées dans le projet :

### 🎒 Système d'inventaire
- Inventaire modulaire dans le style de **Minecraft**
- Support du **clic droit pour diviser les stacks**
- Séparation claire entre **l’affichage** et la **logique** (2 scripts différents)
- Inventaire utilisable par d'autres éléments du jeu comme les **coffres**

### 🚪 Portails
- Portails inter-scènes avec 3 états :
  - `Actif` : utilisable
  - `Cooldown` : temporairement désactivé
  - `Inactif` : verrouillé
- 3 scènes :
  - **Menu principal**
  - **Hub**
  - **Monde de ressources**

### 🧍‍♂️ Mouvement du joueur
- Scripts **modulaires** :
  - Mouvement de base
  - Sprint
  - Dash
  - Stamina
  - Santé
- Chaque script peut être activé/désactivé indépendamment (ex : une IA peut utiliser uniquement le mouvement + dash)

### ⚠️ Pièges
- Pièges avec différents effets :
  - Réduction de la santé
  - Ralentissement du joueur
- Système d’états : actif/inactif

### 💾 Persistance de l'inventaire
- L’inventaire est sauvegardé **d’une scène à l’autre**

### 🧪 Tests unitaires
- **Tests unitaires** sur le système d’inventaire via le framework Unity Test Runner

### 🧰 Système de crafting
- Interface et logique de **craft modulaire**

### ⚙️ Variables exposées
- Toutes les variables importantes sont **facilement configurables** dans l’inspecteur Unity pour faciliter le travail des game designers

---

## 🚀 Lancer le projet

### 🔧 Prérequis

- **Unity 2022.3.17f1** (version utilisée pour le développement)

### 📥 Installation

1. Clonez le dépôt :
   ```bash
   git clone https://github.com/oneblack74/ProjetL3Unity.git
   ```
2. Ouvrez le projet dans Unity.
3. Lancez la scène du Menu Principal pour tester le jeu.

---

## 👥 Équipe
- Brissy Axel
  🔗 [GitHub](https://github.com/oneblack74) - [itch.io](https://oneblack74.itch.io)
- Delcroix Victor
  🔗 [GitHub](https://github.com/LeadCreep) - [itch.io](https://leadcreep.itch.io/)

---

## ✅ Contraintes
- [X] Inventaire
- [X] Portails
- [X] Système de crafting
- [X] Mouvement modulaire
- [X] Tests unitaires
- [X] Pièges
- [X] Sauvegarde d’inventaire

---

## ✨ Objectifs pédagogiques

- Travailler en équipe avec Git
- Appliquer les principes de modularité
- Utiliser les outils de test unitaire dans Unity
- Développer une architecture flexible et réutilisable
