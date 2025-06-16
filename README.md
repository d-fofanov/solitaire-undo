# Solitaire Prototype (Unity)

A small (mostly) AI-built demo of Undo functionality in a Solitaire-like game.
The architecture separates core logic from visuals, with clean interfaces and reusable components.
This foundation is ready for extending into a full game.

---

## 🧩 Entities & Structure

### 🎴 Card Data
- **`CardSuit` / `CardHonor`** — enums for suit and rank
- **`Card`** — immutable instances of all 52 cards, accessible statically by suit/honor

### 🗂️ Game State
- **`CardStack`** — mutable card collection with push/pop, split/merge, validation rules
- **`IReadOnlyCardStack`** — read-only interface for stack views and logic access
- **`CardPlacementRules`** — static rule system for validating legal card moves
- **`MoveResult`** — readonly data snapshot of a single move (before/after stacks, moved cards), ready to be exposed to UI code
- **`CardPlacementHistory`** — move recorder with undo support

### 🕹️ Game Controller
- **`SolitaireGame`** — manages 12 card stacks (4 empty, 8 prefilled), move logic, undo

### 🎨 Dummy UI
- **`CardView`** — Unity `MonoBehaviour` for rendering a card (front/back)
- **`CardPool`** — efficient pooled instantiator of `CardView` objects
- **`CardStackView`** — visual representation of a card stack; tracks changes and updates UI
- **`SolitaireGameUIRoot`** — top-level UI bootstrapper: initializes game, binds stack views

---

## 🧠 AI usage

- ChatGPT 4o did all the work
- All the queries can be found in the [QUERIES.md](QUERIES.md)

---

## ⏱️ Time Spent

- 105 minutes — all logic + first working UI
- 30 minutes — completed UI
- 7 minutes — post-AI fixes
- Total: 2h 22m

---

## 🚀 Next steps

- `SolitaireGame` is ready to be grown into a fully functional game management class
- `MoveResult`&`IReadOnlyCardStack` can be freely passed outside the game logic management
- `MoveResult` has `MovedCards` to allow for cards animation
- An additional 'floating' cards stack can be created and maintained to support drag functionality
