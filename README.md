# ⚔️ Hero Engine - RPG Battle System

✅ **Estat de la versió:** Totes les *issues* han estat tancades i la Pull Request ha estat aprovada i integrada per la branca `razor-files`.

Aquest repositori conté el motor de joc de rol "Hero Engine", implementat en C# amb .NET Core i ASP.NET Core Razor Pages. Inclou la gestió de jerarquies de personatges, sistema de combat per torns amb IA, analítiques de partides i gestió completa de fitxers.

---

## 📂 Estructura del Projecte

El projecte està dividit en diferents capes per respectar els principis d'arquitectura neta i responsabilitat única (SRP):

* **`HeroEngine.Core/`**: Conté la lògica de negoci.
    * `Models/`: Jerarquia de classes (`AHero`, `Mage`, `Warrior`, `Rogue`, `Ability`, `Enemy`).
    * `Combat/`: Motor de batalla (`BattleEngine`) i estadístiques (`BattleStatistics`).
    * `Data/`: Escriptura i lectura de fitxers, integració amb CSV i JSON (`CsvStatsWriter`).
    * `Analytics/`: Consultes LINQ per processar estadístiques (`HeroAnalytics`).
* **`HeroEngine.Web/`**: La interfície d'usuari web (Frontend).
    * `Pages/`: Vistes de Razor Pages (ex. `/Stats`, `/Files`).
    * `wwwroot/`: Recursos estàtics (CSS, imatges, JS).
* **`Data/`**: Carpeta a l'arrel del projecte on es generen i guarden els fitxers de registre i configuració.

## 🚀 Instruccions d'Execució

Per fer funcionar el projecte en un entorn de desenvolupament local, segueix aquests passos:

1.  **Clonar el repositori** i assegurar-se d'estar a la branca principal (on s'ha fusionat `razor-files`).
2.  **Obrir la solució** amb Visual Studio 2022 o Visual Studio Code.
3.  **Restaurar paquets:** Obre la terminal a l'arrel del projecte i executa:
    ```bash
    dotnet restore
    ```
4.  **Executar el projecte:**
    Pots prémer `F5` a Visual Studio, o executar la següent comanda a la terminal apuntant al projecte Web:
    ```bash
    dotnet run --project HeroEngine.Web
    ```
5.  **Accedir a l'aplicació:** Obre el teu navegador web i navega a `https://localhost:5001` (o el port que la teva consola indiqui). Pots visitar les rutes `/Stats` i `/Files` per veure les noves pàgines d'analítica i gestió.

## 📄 Exemples de Fitxers de Dades

El sistema genera i consumeix automàticament els següents fitxers dins de la carpeta `Data/`:

**1. `combat_stats.csv` (Registre de resultats de combats)**
```bash
04/26/2026 15:15:19;Partes, Molen, Lowl;Elite 1, Minion 2;Victory;8;350;Lowl
04/26/2026 15:15:22;Partes, Molen, Lowl;Elite 1;Victory;6;250;Lowl
04/26/2026 15:15:24;Partes, Molen, Lowl;Elite 1;Victory;5;250;Lowl
04/26/2026 15:15:26;Partes, Molen, Lowl;Minion 1, Minion 2, Minion 3, Boss 4, Minion 5;Victory;16;1000;Lowl
04/26/2026 15:15:29;Partes, Molen, Lowl;Minion 1, Elite 2, Minion 3;Victory;5;450;Lowl
```

**2. `heroes.json` (Dades o configuracions en format JSON)**
 ```bash
[
  {
    "Name": "Partes",
    "Type": "Warrior",
    "Level": 1,
    "MaxHp": 150,
    "Power": 30,
    "Defense": 15,
    "Armor": 40,
    "BattleCry": "Mooooo",
    "MaxMana": 0,
    "ArchLevel": 0,
    "Abilities": null,
    "MultDamage": 0,
    "NumDaggers": 0
  },
  {
    "Name": "Molen",
    "Type": "Mage",
    "Level": 1,
    "MaxHp": 200,
    "Power": 20,
    "Defense": 20,
    "Armor": 0,
    "BattleCry": null,
    "MaxMana": 80,
    "ArchLevel": 1,
    "Abilities": [
      {
        "Name": "Fireball",
        "Type": "Attack",
        "Rarity": "COMMON",
        "Cost": 5,
        "AbilityPower": 0
      },
      {
        "Name": "Magic Shield",
        "Type": "Defense",
        "Rarity": "RARE",
        "Cost": 15,
        "AbilityPower": 0
      },
      {
        "Name": "Heal",
        "Type": "Healing",
        "Rarity": "COMMON",
        "Cost": 5,
        "AbilityPower": 0
      }
    ],
    "MultDamage": 0,
    "NumDaggers": 0
  },
  {
    "Name": "Lowl",
    "Type": "Rogue",
    "Level": 1,
    "MaxHp": 170,
    "Power": 15,
    "Defense": 25,
    "Armor": 0,
    "BattleCry": null,
    "MaxMana": 0,
    "ArchLevel": 0,
    "Abilities": null,
    "MultDamage": 3,
    "NumDaggers": 10
  }
]
```

**3. `battleLog.txt` (Log detallat dels esdeveniments per torns)**
```bash
======================================================
               BATTLE LOG INITIALIZED              
                    BATTLE STARTS                     
======================================================


============== BATTLE LOG - Round 1 ===============
[HERO] Activating 'Magic Shield' [RARE]...
[HERO] Molen shields itself from 40 damage.
[ENEMY] Minion 1 receives 0 damage -> Defense: 10 -> Actual damage: 0 | 100/100
[HERO] Lowl attacks! -> Base damage: 33, Multiplier: 3 -> Deals 99 damage.
[ENEMY] Minion 1 receives 99 damage -> Defense: 10 -> Actual damage: 89 | 11/100
[HERO] Partes attacks! Deals 70 damage.
[ENEMY] Minion 1 receives 70 damage -> Defense: 10 -> Actual damage: 60 | 0/100
[ENEMY] Elite 2 attacks! Deals 50 damage.
[HERO] Partes receives 50 damage. -> Defense: 33, Armor absorbs 40 damage -> Total damage: 0 | HP: 725/725
[ENEMY] Minion 3 attacks! Deals 20 damage.
[HERO] Partes receives 20 damage. -> Defense: 33, Armor absorbs 40 damage -> Total damage: 0 | HP: 725/725

============== BATTLE LOG - Round 2 ===============
[HERO] Activating 'Magic Shield' [RARE]...
[HERO] Molen shields itself from 40 damage.
[ENEMY] Elite 2 receives 0 damage -> Defense: 20 -> Actual damage: 0 | 250/250
[HERO] Lowl attacks! -> Base damage: 33, Multiplier: 3 -> Deals 99 damage.
[ENEMY] Elite 2 receives 99 damage -> Defense: 20 -> Actual damage: 79 | 171/250
[HERO] Partes attacks! Deals 70 damage.
[ENEMY] Elite 2 receives 70 damage -> Defense: 20 -> Actual damage: 50 | 121/250
[ENEMY] Elite 2 attacks! Deals 50 damage.
[HERO] Partes receives 50 damage. -> Defense: 33, Armor absorbs 40 damage -> Total damage: 0 | HP: 725/725
[ENEMY] Minion 3 attacks! Deals 20 damage.
[HERO] Partes receives 20 damage. -> Defense: 33, Armor absorbs 40 damage -> Total damage: 0 | HP: 725/725

============== BATTLE LOG - Round 3 ===============
[HERO] Activating 'Fireball' [COMMON]...
[HERO] Molen deals massive damage -> Total damage: 71
[ENEMY] Elite 2 receives 71 damage -> Defense: 20 -> Actual damage: 51 | 70/250
[HERO] Lowl attacks! -> Base damage: 33, Multiplier: 3 -> Deals 99 damage.
[ENEMY] Elite 2 receives 99 damage -> Defense: 20 -> Actual damage: 79 | 0/250
[HERO] Partes attacks! Deals 70 damage.
[ENEMY] Minion 3 receives 70 damage -> Defense: 10 -> Actual damage: 60 | 40/100
[ENEMY] Minion 3 attacks! Deals 20 damage.
[HERO] Partes receives 20 damage. -> Defense: 33, Armor absorbs 40 damage -> Total damage: 0 | HP: 725/725

============== BATTLE LOG - Round 4 ===============
[HERO] Activating 'Fireball' [COMMON]...
[HERO] Molen deals massive damage -> Total damage: 71
[ENEMY] Minion 3 receives 71 damage -> Defense: 10 -> Actual damage: 61 | 0/100

======================================================
                   THE HEROES WIN!                    
======================================================
[SYSTEM] The experience of the battle makes the suvivors stronger!
[SYSTEM] Partes levelled up to Level 6!
[SYSTEM] Molen levelled up to Level 6!
[SYSTEM] Lowl levelled up to Level 6!
======================================================

======================================================
                  BATTLE STATISTICS                   
======================================================
[SYSTEM] Total damage dealt by the heroes: 450
[SYSTEM] Most effective hero (most damage dealt): Lowl (238 damage)
[SYSTEM] Fastest enemy defeated: Minion 1 (defeated in round 1)
```
---
# 🛡️ Chapter 1: El Consell dels Herois — Jerarquia de classes

Aquest readme detalla els resultats d'executar un joc de proves a la clase base AHero i les subclasses Warrior, Mage i Rogue. 

---

## 1. Estat inicial (Greeting / ToString)

Creant els personatges dona els següents resutalts:  

Warrior: [Warrior] Link | Level: 1 | HP: 150/150 | Power: 30 | Armor: 40 | Battle Cry: 'Hyaa!'  
Mage: [Mage] Ela | Level: 1 | HP: 200/200 | Power: 20 | Mana: 40/40 | Arch level: 1  
Rogue: [Rogue] Elding | Level: 1 | HP: 170/170 | Power: 15 | Damage multiplier: 3 | Daggers: 10  

## 2. Proves d'atac

Atacant amb els personatges dona els següents resultats:  

> Warrior: Link attacks! Deals 30 damage.  
Mage: Ela attacks! Deals 20 damage.  
Rogue: Elding attacks! -> Base damage: 15, Multiplier: 3 -> Deals 45 damage.  

## 3. Proves de dany rebut

Rebent dany amb Mage i Rogue dona els següents resultats:  

> Mage: Ela receives 30 damage. | HP: 170/200  
Rogue: Elding receives 30 damage. | HP: 140/170  

Atacant al Warrior amb 50 de dany (Armor: 40):  

> Warrior: Link receives 50 damage. -> Armor absorbs 40 damage -> Total damage: 10 | HP: 140/150  

Atacant al Warrior amb 30 de dany (Armor: 40):  

> Warrior: Link receives 30 damage. -> Armor absorbs 40 damage -> Total damage: 0 | HP: 140/150  

## 4. Proves de derrota

El Rogue rep suficient dany com per ser derrotat:  

> Rogue: Elding receives 200 damage. | HP: 0/170  

Si s'intenten fer accions amb el Rogue derrotat:  

> Rogue: Elding is defeated and can't attack!  
Rogue: Elding is defeated and can't receive damage!  


# 🔮 Chapter 2: El Grimori — Sistema d'Habilitats i Màgia

Aquest readme detalla els resultats d'executar el joc de proves per a la classe `Mage` i la seva interacció amb la gestió i llançament d'objectes `Ability`.

---

## 1. Estat inicial (Inventari buit)

Comprovant la llista d'habilitats del personatge Ela just després de ser creada dona el següent resultat:

> Ela has no abilities equiped.

## 2. Proves d'equipament d'habilitats

Equipant noves habilitats a la maga dona els següents resultats:

> [System] Ela equipped 'Fireball'  
> [System] Ela equipped 'Ice shield'  
> [System] Ela equipped 'Mega heal'  
> [System] Ela equipped 'Attack up'  

Si s'intenta equipar una habilitat que ja posseeix a la seva llista:

> [System] Ela has already 'Fireball' equiped

## 3. Llistat d'habilitats (Ordenació i Alineació)

Llistant les habilitats equipades, el sistema les mostra ordenades ascendentment segons el valor de la seva raresa:

> ======================================================  
Ela's Abilities  
======================================================  
[COMMON]       Ice shield         | Type: Defense  | Cost:  5 mana  
[RARE]         Fireball           | Type: Attack   | Cost: 15 mana  
[EPIC]         Mega heal          | Type: Healing  | Cost: 25 mana  
[LEGENDARY]    Attack up          | Type: Support  | Cost: 40 mana  
======================================================  

## 4. Proves de dany i llançament de curació

La maga rep dany per reduir la seva vida i posteriorment llança una habilitat de curació, respectant sempre el límit de la seva vida màxima (200 HP):

> [Mage] Ela | Level: 1 | HP: 160/200 | Power: 20 | Mana: 80/80 | Arch level: 1  
> Activating 'Mega heal' [EPIC]...  
> Ela heals 65 damage.  
> [Mage] Ela | Level: 1 | HP: 200/200 | Power: 20 | Mana: 55/80 | Arch level: 1  

## 5. Proves de llançament de defensa i atac

Es fan llançaments consecutius de defensa (que n'augmenten l'estadística) i d'atac (que sumen el poder base de la maga més el de l'habilitat). Els recursos de manà s'esgoten progressivament:

> [Mage] Ela | Level: 1 | HP: 200/200 | Power: 20 | Mana: 55/80 | Arch level: 1  
> Activating 'Ice shield' [COMMON]...  
> Ela shields itself from 25 damage.  
> [Mage] Ela | Level: 1 | HP: 200/200 | Power: 20 | Mana: 50/80 | Arch level: 1  
> Activating 'Fireball' [RARE]...  
> Ela deals massive damage -> Total damage: 60  
> [Mage] Ela | Level: 1 | HP: 200/200 | Power: 20 | Mana: 35/80 | Arch level: 1  

## 6. Proves d'esgotament de Manà

La maga intenta llançar la seva habilitat Llegendària 'Attack up' (Cost: 40), però només li queden 35 punts de manà després del combat anterior, fent que l'acció falli:

> Ela can't cast 'Attack up' beacuse it doesn't have enough mana! (Needs 40, has 35)  


# ⚔️ Chapter 3: El Camp de Batalla — Motor de Combat i Intel·ligència Artificial

Aquest readme detalla els resultats d'executar el joc de proves per al motor de combat (`BattleEngine`), la interacció entre les faccions, la presa de decisions (IA) i el càlcul d'estadístiques finals (`BattleStatistics`).

---

## 1. Preparació i Inici del Combat

S'instancien els herois (Grog el Guerrer i Merlín el Mag) contra els enemics (un Elit i un Esbirro). Abans de començar, el sistema registra l'equipament d'habilitats i obre el fitxer de registre d'esdeveniments.

> [System] Merlin equipped 'Bola de Fuego'  
[System] Merlin equipped 'Cura Menor'  
[System] Merlin equipped 'Aura Arcana'  
======================================================  
   COMBAT LOG INITIALIZED: 24/10/2023 10:00:00  
======================================================  
                    BATTLE STARTS                     
======================================================  

## 2. Iniciativa i Presa de Decisions (IA del Mag)

Durant la primera ronda, l'ordre d'atac es regeix per la Iniciativa. El mag avalua el seu estat: com que té el manà i la salut alts, la seva IA escull llançar un encanteri de suport. El guerrer absorbeix els atacs enemics gràcies a la sobreescriptura de la seva armadura.

> ============== BATTLE LOG - Round 1 ===============  
[HERO] Activating 'Aura Arcana' [RARE]...  
[HERO] Merlin increases its power.  
[HERO] Grog attacks! Deals 30 damage.  
[ENEMY] Elite Segfault receives 15 damage. | HP: 105/120  
[ENEMY] Elite Segfault attacks! Deals 25 damage.  
[HERO] Grog receives 25 damage. -> Armor absorbs 40 damage -> Total damage: 0 | HP: 150/150  
[ENEMY] Minion Bug attacks! Deals 15 damage.  
[HERO] Grog receives 15 damage. -> Armor absorbs 40 damage -> Total damage: 0 | HP: 150/150  

## 3. Canvi de comportament i Polimorfisme d'Objectius

A la segona ronda, el manà del mag ha baixat, provocant que la seva IA descarti el suport i prioritzi l'atac. Quan l'Elit cau derrotat, el sistema de focalització (`GetTarget`) redirigeix automàticament els atacs de la resta d'herois cap a l'esbirro viu sense llançar errors.

> ============== BATTLE LOG - Round 2 ===============  
[HERO] Activating 'Bola de Fuego' [EPIC]...  
[HERO] Merlin deals massive damage -> Total damage: 85  
[ENEMY] Elite Segfault receives 70 damage. | HP: 35/120  
[HERO] Grog attacks! Deals 30 damage.  
[ENEMY] Elite Segfault receives 15 damage. | HP: 20/120  
 
> ============== BATTLE LOG - Round 3 ===============   
[HERO] Activating 'Bola de Fuego' [EPIC]...  
[HERO] Merlin deals massive damage -> Total damage: 85  
[ENEMY] Elite Segfault receives 70 damage. | HP: 0/120  
[ENEMY] Elite Segfault is defeated and can't attack!  
[HERO] Grog attacks! Deals 30 damage.  
[ENEMY] Minion Bug receives 25 damage. | HP: 25/50  

## 4. Victòria i Pujada de Nivell

Un cop el sistema detecta que tots els integrants de la llista `_enemies` estan derrotats (`IsDefeated`), el combat finalitza. La classe `BattleEngine` aplica el càsting segur per augmentar el nivell només dels herois supervivents.

> ======================================================  
                   THE HEROES WIN!                    
======================================================  
[SYSTEM] The experience of the battle makes the suvivors stronger!  
[SYSTEM] Grog levelled up to Level 2!  
[SYSTEM] Merlin levelled up to Level 2!  
======================================================  

## 5. Estadístiques del Combat (Classe Auxiliar)

En finalitzar la batalla, la classe de responsabilitat única `BattleStatistics` s'encarrega d'analitzar els diccionaris de dany real guardats durant els torns, mostrant el rendiment de l'equip.

> ======================================================  
                  BATTLE STATISTICS                   
======================================================  
[SYSTEM] Total damage dealt by the heroes: 170  
[SYSTEM] Most effective hero (most damage dealt): Merlin (115 damage)  
[SYSTEM] Fastest enemy defeated: Elite Segfault (defeated in round 3)  
======================================================  