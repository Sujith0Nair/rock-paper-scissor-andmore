# Game Improvement Ideations

This document outlines high-level strategies and features to elevate "Rock-Paper-Scissors-Lizard-Spock" from a simple PvE prototype to a fully-featured, engaging multiplayer experience.

## 1. Multiplayer Integration (Unity Netcode)

The most significant upgrade is transforming the single-player experience into a real-time PvP competitive game using **Unity Netcode for GameObjects (NGO)**.

### Architecture
*   **Host-Client Topology:** For a turn-based game like this, a Host-Client architecture is cost-effective and sufficient. One player acts as the host, and the other joins.
*   **Unity Services:** Leverage Unity Gaming Services (UGS) to handle connectivity without needing dedicated IP addresses.
    *   **Unity Transport:** The low-level transport layer.
    *   **Unity Relay:** Ensures players can connect even behind firewalls/NATs without port forwarding.

### Features
*   **Friend Invites & Private Lobbies:**
    *   Use **Unity Lobby** service to create private rooms.
    *   Generate a unique "Room Code" (e.g., "ABCD") that players can share via messaging apps to invite friends directly.
*   **Random Matchmaking:**
    *   Implement a "Quick Play" button using **Unity Matchmaker** (or a simple Lobby query system) to pair players looking for a game.
    *   Match players based on a hidden Elo rating to ensure fair competition.
*   **Emote System:** Allow players to send preset emotes (e.g., "Good Game", "Lucky!", "Thinking...") during the countdown to increase social interaction.

## 2. Meta-Game & Progression

To retain players long-term, we need to add layers of progression outside the core gameplay loop.

### Global Leaderboards
*   **Implementation:** Use **Unity Cloud Leaderboards** or a backend like **PlayFab** / **Firebase**.
*   **Categories:**
    *   *Weekly Champions:* Resets every week to keep the competition fresh.
    *   *All-Time Wins:* Rewards veteran players.
    *   *Win Streaks:* Highlights players with the highest consecutive wins.

### Player Profile & Customization
*   **Hand Skins:** Unlockable visual variants for the hands (e.g., "Golden Rock", "Sci-Fi Laser Gun" for Scissors, "Alien Spock").
*   **Titles & Avatars:** Earned by completing specific challenges (e.g., "The Logician" for winning 10 games with Spock).
*   **Currency:** Earn "Coins" per win to purchase cosmetic items.

## 3. Gameplay Enhancements

Expanding the core mechanics to add depth and variety.

### Game Modes
*   **Best of 3 / 5:** Standard competitive format to reduce luck variance.
*   **Time Attack:** Players have only 1-2 seconds to make a decision, increasing the pressure and error rate.
*   **"Blind" Mode:** The opponent's last played hand is hidden, removing the ability to predict based on patterns.

### Mechanics
*   **Special Abilities (Power-ups):**
    *   *Peek:* Reveal the opponent's choice for 0.5 seconds (Cost: High currency/cooldown).
    *   *Freeze:* Reduce the opponent's timer speed.
    *   *Second Chance:* If you lose, instantly trigger a tie-breaker round instead (One-time use).

## 4. Technology & Infrastructure

*   **Addressables:** Convert Hand assets and UI textures to Addressables. This allows for remote content updates (new skins/seasonal events) without forcing a full app update.
*   **Analytics:** Integrate **Unity Analytics** to track:
    *   Most/Least picked hands (Balancing).
    *   Average session length.
    *   Drop-off points in the tutorial.
*   **Anti-Cheat:** Basic server-side validation (or Host validation) to ensure players can't send invalid moves or freeze their timers.

## 5. UI/UX Polish

*   **Haptic Feedback:** Vibrations when the timer ticks down or when a hand "smashes" another.
*   **Dynamic Camera:** Slight camera shake on impact; zoom in on the winning hand.
*   **Sound Design:** Distinct SFX for each interaction (e.g., a "zap" sound when Spock vaporizes Rock, a "crunch" when Rock crushes Scissors).
