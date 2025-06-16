
# The Last Protocol

## Tema jocului

Tema jocului nostru este despre un robot care luptă să repare lumea în urma unei anomalii temporale care a făcut toți oamenii să dispară în prezent și trecut.

Datorită acestei anomalii robotul capătă abilitatea de a călători în timp în două momente, unul cu decenii în trecut și unul cu decenii în viitor.

El călătorește prin lume și se luptă cu monștri temporali pentru a ajunge la sursa anomaliei, în speranța că va putea să își salveze creatorii.


## Demo

https://youtu.be/0_NPPoJl36c

## Ce poate face jucătorul?

### Controlarea personajului

- Se poate mișca stânga - dreapta
- Poate sări
- Poate coborî de pe platformă
- Poate ataca inamici
- Poate activa un checkpoint
- Poate vizualiza un moment dintr-un timp diferit de cel în care se află
- Poate călători în timp

### Interacționarea cu meniul

- Meniul Principal
    - Poate porni jocul, selectând unul din cele 3 slot-uri disponibile pentru salvarea progresului. Progresul poate fi șters din fiecare slot
    - Poate accesa Meniul de Setări
    - Poate ieși din joc

- Meniul de Pauză
    - Îl poate accesa apăsând tasta "Esc" în timpul jocului
    - Poate merge la ultimul checkpoint
    - Poate accesa Meniul de Setări
    - Poate accesa Meniul Principal
    - Poate ieși din joc

- Meniul de Setări
    - Îl poate accesa din Meniul Principal sau din Meniul de Pauză
    - Poate schimba rezoluția și poate comuta modul ecran complet
    - Dacă îl accesează din Meniul de Pauză, pe lângă acțiunile menționate mai sus, poate seta propriile butoane pentru controlul personajului și interacționarea cu meniurile

## Input

Jocul suportă input de la tastatură sau controller.

## Backlog

- Implementarea funcțiilor necesare pentru controlarea personajului
    ([mișcarea](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/playerMovement.cs#L103-L148),
    [atacul](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PlayerShoot.cs#L41-L51),
    [vizualizarea în timp](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/TimeSightOverlay.cs),
    [călătoria în timp](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/GameController.cs#L88-L213))
- Implementarea [gravitației](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/playerMovement.cs#L91-L102)
- Implementarea funcțiilor necesare pentru interacționarea cu meniurile
    (butoanele [Start Game](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/StartCanvas.cs#L14-L23),
    [Settings](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/SettingsScript.cs),
    [Exit](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/StartCanvas.cs#L24-L31),
    [Resume](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PauseMenu.cs#L24-L28),
    [Last Checkpoint](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PauseMenu.cs#L29-L33),
    [Main Menu](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PauseMenu.cs#L39-L49) și
    [Slot-urile](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/SaveData.cs)
    \- fiecare cu un buton [Delete](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/StartCanvas.cs#L36-L43))
- Implementarea funcțiilor necesare pentru gestionarea proprietăților 
    ([viețile și energia* personajului](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PlayerHealth.cs),
    [viețile inamicilor](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/Enemies.cs#L118-L130)) <br>
    \* energia personajului mai este gestionată și în funcțiile pentru atac și pentru călătoria în timp
- Implementarea funcțiilor necesare pentru gestionarea [acțiunilor inamicilor](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/Enemies.cs) (mișcare, atac)
- Implementarea unui sistem pentru [salvarea progresului](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/SaveData.cs)
- Adăugarea unor [instrucțiuni](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/CutsceneTrigger.cs) pe care jucătorul să le vadă în timpul jocului
- Adăugarea [efectelor sonore](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/SoundManager.cs) și a [melodiei de fundal](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/PersistentMusic.cs)
- Adăugarea [efectului de parallax](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/BackgroundParallax.cs)
- Adăugarea și gestionarea [coliziunilor între personaj și platforme sau obiecte](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/SelfBox.cs) a celorlaltor [coliziuni](https://github.com/SerbencuCristian/EpicTImeTravelGame/blob/main/Testgame/Assets/Scripts/Boxes.cs)
- Crearea și animarea sprite-urilor

## Prompt Engineering

Am folosit ChatGPT și GitHub Copilot, unde am pus întrebări (ex. "How should i go about adding this?") care ne-au ajutat să aflăm despre metodele corecte de a implementa anumite aspecte (cum ar fi manager-ul de sunet) și să remediem bug-urile. De asemenea, funcția de inline autocomplete a accelerat producția, în special prin apelarea corectă a funcțiilor corespunzătoare contextului. <br>

Pe de altă parte, deși AI-ul este folositor, nu le poate face pe toate: la începutul proiectului, am încercat să facem sprite-uri cu ajutorul său, dar a trebuit să abandonăm ideea - și le-am făcut de mână.

## GITHUB TEAM PROJECT

https://github.com/users/SerbencuCristian/projects/1
