# Rock, Paper, Scissors, Lizard, Spock

A modern variant of the classic Rock, Paper, Scissors game, adding two extra elements: Lizard and Spock. This single-player (PvE) strategy game challenges you to beat the AI in a test of quick thinking and luck.

## Game Rules

The rules are an extension of the classic game. Each element defeats two others:

*   **Scissors** cuts **Paper**
*   **Paper** covers **Rock**
*   **Rock** crushes **Lizard**
*   **Lizard** poisons **Spock**
*   **Spock** smashes **Scissors**
*   **Scissors** decapitates **Lizard**
*   **Lizard** eats **Paper**
*   **Paper** disproves **Spock**
*   **Spock** vaporizes **Rock**
*   **Rock** crushes **Scissors**

## Credits

*   **Art:** All the artwork for this project was created by **Nikunj Rallabhandi**. You can find more of his work at his portfolio: [nikunjrallabhandi.framer.website](https://nikunjrallabhandi.framer.website/).
*   **Music:** Song: NEYVO - My Heart Is Broken [NCS Release]. Music provided by NoCopyrightSounds. Free Download/Stream: [http://ncs.io/MHIB](http://ncs.io/MHIB).

## Requirements

*   **Unity Version:** 6000.3.2f1 (Unity 6)

## Game Logic Breakdown

The core gameplay loop is managed by the `GameRoundManager`.

1.  **Round Start:** A new round begins, and a countdown timer (default 5 seconds) starts.
2.  **Player Input:** The player selects their hand (Rock, Paper, Scissors, Lizard, or Spock) before the timer expires.
3.  **AI Input:** The computer randomly selects a hand (hidden until the reveal).
4.  **Resolution:**
    *   When the timer ends or the player locks in their choice, the hands are compared.
    *   **Win:** If the player wins, their score increases, and a "Player Won" popup appears. The game continues to the next round.
    *   **Lose/Timeout:** If the player loses or fails to choose in time, a "Computer Won" popup appears, and the game ends, returning to the Main Menu.
    *   **Draw:** A "Draw" popup appears, and the round restarts.
5.  **Scoring:** The score tracks the number of continuous rounds won in the current session.

## Architecture & Code Patterns

The project utilizes a decoupled, data-driven architecture using **ScriptableObjects**.

### Scriptable Object Architecture for Events

Communication between different systems (e.g., Game Logic to UI, Game Logic to Scene Management) is handled via **Event Channels**. This "Event Bus" pattern ensures that systems remain loosely coupled.

*   **`GameEventChannel`:** Handles high-level game state changes, such as requesting a scene change (e.g., returning to the Main Menu).
*   **`PopupEventChannel`:** Manages the display of UI popups (Win, Lose, Draw). The Game Manager simply "raises" a popup event, and the UI Manager listens and responds. This allows the game logic to remain agnostic of the specific UI implementation.

### Data-Driven Hands & Operator Overloading

The game elements (Hands) are defined as `ScriptableObjects`. This makes it easy to add or modify hand types without changing the code.

*   **`Hand` Class:** Each hand (Rock, Paper, etc.) is an asset containing its properties (Name, Sprite, Strong Against).
*   **Operator Overloading:** The logic for determining the winner is encapsulated elegantly within the `Hand` class using C# operator overloading.
    *   `handA > handB` returns `true` if `handA` defeats `handB`.
    *   This simplifies the game manager logic to a readable check: `if (playerHand > computerHand) { ... }`.

### Configuration

*   **`HandsHolder`:** acts as a central repository for all available Hand assets, allowing easy retrieval by type.

## Future Enhancements

If you are interested to learn more about the future enhancements of the game (Multiplayer, Leaderboards, etc.), please refer to the [IDEATION.md](IDEATION.md) file.
