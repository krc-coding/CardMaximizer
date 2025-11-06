# Game Design Document

## 1 game overview

**Titel:**  
Card Maximizer

**Genre:** Procedural Puzzle Card Game

**Platform:**  
PC

**Short description of the game:**  
It's a game about winning, over the opponent, but also winning over the rules, as the rules aren't gaurenteed.  
All you know is that there will be rules, but not which.

---

## 2 Games purpose

It's about having fun, playing without knowing what the next game will be like.  
Everything in the game is based on the rules drawn, and is all about beating the rules drawn for a game.

---

## 3 Core Gameplay Loop

The core of the game is the randomly drawn rules at the start of each game, making it so that no 2 games should be
identical.  
But the one thing that is consistent across all games, is that you're trying to beat the rules.

**The player's repeated actions:**

1. Player starts a new game → Random rules are drawn and revealed
2. Player receives initial hand of cards
3. Player and AI opponent take turns playing cards according to the drawn rules
4. Game evaluates win/loss condition based on the active rules
5. Player receives feedback on performance and returns to step 1 with new rules

**Core Loop:**

- Draw rules → Learn rules → Play strategically → Adapt to rules → Win or lose → Draw new rules

The satisfaction comes from both mastering individual rule sets and developing the skill to quickly adapt to unknown
combinations.

---

## 4 Regler og Mekanikker

**Meta-Rules (Always Apply)**:

- At the start of each game, one rule group (Game Mode) is selected, and all rules from that group are applied
- Additionally, 0-3 random modifier rules are selected from the extra rule pool
- All rules are revealed to both player and AI before the first turn begins
- The player always goes first
- Players alternate turns: Player → AI → Player → AI (repeat)
- Each turn, players automatically draw cards based on the "Draw Amount" rule (default: 1 card)
- Players have a limited number of actions per turn (determined by active rules)
- Both drawing additional cards and playing cards consume one action each
- The game ends when a win condition (defined by the active Game Mode) is met

**Game Modes (Rule Groups)**:  
Each game uses ONE of these complete rule sets:

🗡️ **Combat Mode**:

- Win Condition: Reduce opponent to 0 life
- Cards deal 1 damage - Standard numbered cards deal 1 damage to opponent
- Picture cards deal 2 damage - Jacks, Queens, Kings deal 2 damage total (not additional)
- Aces heal 2 life - Playing an Ace restores 2 life to you ⚠️ [Currently bugged]
- Blank cards (Jokers) reflect damage back - Reflects opponent's damage back at them ⚠️ [Currently bugged]
- 4 starting cards - Both players begin with 4 cards in hand
- Start with 20 life - Both players begin with 20 life points
- 1 card per turn - Players can perform 1 action per turn (draw OR play)

🏁 **Race Mode**:

- Win Condition: First player to reach 50 points wins
- Cards give 2 points - Each numbered card played awards 2 points
- Picture cards multiply your score - Each picture card in play multiplies your total points by 2 (multiplicative
  stacking)
- Blank cards (Jokers) do nothing - No point value or effect
- You can play up to 3 cards per turn - Players have 3 actions per turn
- You start with 1 card - Both players begin with only 1 card in hand

⚙️ **Modifier Rules (0-3 randomly selected)**:

- Draw Amount: Automatically draw 2-4 cards at the start of each turn (modifies the automatic draw)
- Hand Size Modifier: Maximum hand size adjusted by -2 to +3 cards
- Point Modifier: Base point values multiplied by 0.5-2.0× (affects Race Mode scoring)
- Turn Timer: 10/15/20/30 second time limit per turn - turn automatically ends when timer expires

**Current Known Issues**:

- ⚠️ "Aces heal 2 life" rule not functioning (Combat Mode)
- ⚠️ "Blank cards reflect damage" rule not functioning (Combat Mode)
- ⚠️ Combat Mode rules are less balanced than Race Mode
- ⚠️ Running out of cards in deck causes soft lock (cards should recycle from played cards, not yet implemented)

**Core Mechanics**:

- Turn-based card play: Player and AI alternate playing cards from their hands
- Action system: Both drawing extra cards and playing cards consume actions
- Automatic draw phase: At turn start, automatically draw cards based on active rules
- Hand management: Limited hand size and actions force strategic choices
- Rule adaptation: Success requires understanding how the active rules interact

💬 *The challenge comes from quickly understanding how the randomly selected Game Mode and Modifiers work together, then
adapting your strategy accordingly.*

---

## 5 Spilverden og Tema

**Setting:**  
The game takes place on a worn wooden table in a cozy game enthusiast's home. The environment feels lived-in and
nostalgic, with incomplete board games on shelves in the background, dice scattered around, and warm ambient lighting
from a nearby lamp.

**Atmosphere:**

- Relaxed and contemplative, like a quiet evening of cards
- Slightly mysterious due to the unknown rules each game
- Aged aesthetic inspired by *Inscryption* - weathered cards, vintage furniture
- Comfortable and inviting, not stressful or rushed

**Visual Style:**

- Warm, muted color palette (browns, creams, soft lighting)
- Cards show natural wear and aging
- UI elements have a hand-written or stamped appearance
- Soft shadows and gentle animations

💬 *The theme supports the gameplay by creating a space where experimentation feels safe and discovery feels rewarding.
The "incomplete games" theme justifies why rules are always changing - you're playing with mixed rule sets from
different games.*

---

## 6 Figurer og Objekter

**The Player**:

- Represented by the bottom area of the table
- Views their hand as card sprites at the bottom of screen
- Drags and drops cards to play area or discard pile
- Must decide each turn whether to draw more cards or play cards (both consume actions)
- Has a life total (Combat Mode) or score (Race Mode) displayed [⚠️ exact UI position TBD]
  The AI Opponent:
- Sits at the opposite side of the table (top)
- Follows identical rules as the player
- Makes strategic decisions based on active rule set
- Current Alpha Limitations:
    - ⚠️ AI's card count is not displayed to player
    - ⚠️ AI's hand is not visible (working as intended)
    - ⚠️ AI's life/score display may be missing/placeholder

**Cards**:

- Standard 52-card deck plus 2 Jokers ("Blank Cards")
- Visual categories:
    - Numbered cards (2-10): Different values/effects based on active rules
    - Picture cards (J, Q, K): Special effects in both game modes
    - Aces: Special healing effect in Combat Mode
    - Jokers/Blank Cards: Rule-dependent special effects
- Cards can be dragged from hand to play area or discard pile
- Current Alpha State:
    - ⚠️ Cards played to central area are not actually displayed there (functionality works, visuals missing)

**Game Board Objects**:
**Currently Implemented**:

- Player Hand (Bottom): Visual display of player's cards, draggable
- Play Area (Center): Drop zone for playing cards (⚠️ doesn't show played cards yet)
- Draw Pile (Right): Stack showing remaining cards in deck
- Discard Pile (Left): Stack showing discarded cards
- Rules Popup: Displays all active rules for current game

**Not Yet Implemented**:

- ⚠️ Opponent's hand area (card count display)
- ⚠️ Life/Score tracking UI for both players
- ⚠️ Turn indicator (whose turn it is)
- ⚠️ Action counter (how many actions remaining this turn)
- ⚠️ Turn timer visual (when Turn Timer rule is active)
- ⚠️ Visual feedback for played cards in central area
- ⚠️ Win/loss screen with rule summary

**Visual Style**:

- Wooden table surface as play area
- Aged card aesthetic (using available card graphics)
- (More polish planned for beta)

💬 *The alpha focuses on core mechanics and rule functionality. Visual feedback and UI polish are planned for future
iterations.*

---

## 7 UI og Kontrol

**Controls:**

- **Mouse:** Primary input method
    - Click and drag cards from hand to play them
    - Click buttons for special actions
    - Hover over rules for detailed explanations
- **Keyboard Shortcuts:**
    - ESC: Pause/Menu
    - Space: Confirm action/End turn
    - Tab: Highlight active rules

**UI Elements:**

- **Rule Panel (Side):**
    - Lists all active rules for the current game
    - Highlighted when a rule becomes relevant
    - Tooltip on hover for clarification
- **Player Hand (Bottom):**
    - Arc of cards the player can play
    - Cards highlight when legal to play
- **Status Bar (Top):**
    - Turn counter
    - Current score/progress
    - Special action indicators
- **Opponent Area (Top):**
    - Number of cards in opponent's hand
    - Opponent's score/progress

**Feedback:**

- Cards glow when they're legal to play
- Gentle shake animation for illegal moves
- Sound and visual effect when rules are satisfied
- Clear victory/defeat screen with rule summary

💬 *The UI prioritizes clarity - players need to quickly understand the rules and see what actions are available.*

---

## 8 Level Design og Progression

**Current Alpha State: No Progression System**  
The alpha build currently treats each game as completely standalone:

- No statistics tracking between games
- No unlockable rules
- No difficulty settings
- No tutorial or onboarding
- No persistent data

**Single Game Structure**:

**Game Setup (5-10 seconds)**:

- One Game Mode (Combat or Race) is randomly selected with all its rules
- 0-3 Modifier rules are randomly added
- Rules are displayed to player via popup
- Game begins immediately

**Mid-Game (2-5 minutes)**:

- Players alternate turns rapidly
- Game state is always visible (hand, piles, rules)
- Tension builds as win conditions approach
- No save/resume functionality

**Game End**:

- Victory or defeat screen (placeholder/basic implementation)
- Game returns to main menu or restarts immediately (TBD implementation)

**Planned for Future Versions**:

**Tutorial System (High Priority)**:

- ⚠️ Currently missing - players are thrown directly into games
- Planned: Step-by-step explanation of each Game Mode
- Planned: Interactive guide showing how actions work
- Planned: Sample games with simple rule sets

**Long-term Progression (Post-Alpha)**:

- Statistics tracking (win rate, games played, favorite modes)
- Rule unlocks (start with 2 game modes, unlock more through play)
- Difficulty settings (AI skill adjustment)
- Daily challenges (pre-set rule combinations)

**Current Game Length**:

- Typical game: Under 5 minutes
- Combat Mode: Usually 3-4 minutes
- Race Mode: Usually 2-3 minutes (faster due to more actions)
- Games rarely exceed 5 minutes due to turn timer option

**Known Pacing Issues**:

- ⚠️ Combat Mode can feel slow with only 1 action per turn
- ⚠️ Race Mode imbalance can lead to very short games
- ⚠️ Running out of deck cards causes soft lock (breaks game pacing completely)
- ⚠️ No feedback on "how close was the game" after victory/defeat

💬 *The alpha prioritizes testing core rule interactions. Progression, tutorials, and meta-game systems are planned for
beta and beyond.*

---

## 9 Lyd og Musik

**Music:**

- **Main Menu:** Calm, slightly mysterious acoustic melody
- **During Game:** Soft background ambience with gentle instrumental layers
- **Victory:** Uplifting but not overpowering musical sting
- **Defeat:** Gentle, accepting tune (not punishing)
- **Music Philosophy:** Never stressful or rushed, supports contemplative play

**Sound Effects:**

- **Card Actions:**
    - Soft whoosh when drawing cards
    - Satisfying "thunk" when playing a card
    - Shuffle sound when deck is prepared
    - Crisp snap when discarding

- **UI Sounds:**
    - Gentle click for button presses
    - Soft chime when hovering valid actions
    - Low tone for invalid moves (not harsh)

- **Rule Feedback:**
    - Pleasant bell when satisfying a rule condition
    - Distinctive sound for each type of special action
    - Ambient "discovery" sound when rules are revealed

- **Opponent Actions:**
    - Subtle sounds for AI card plays
    - Different tone than player actions (for clarity)

**Audio Design Philosophy:**

- Sounds are informative but never annoying
- Create atmosphere of a real card game (soft shuffles, gentle card placement)
- Audio cues help players understand game state without looking
- Volume balanced so music doesn't compete with sound effects

💬 *Sound design reinforces the relaxed, thoughtful atmosphere while providing clear gameplay feedback.*

---

## 10 Game Design Overvejelser

### 🎯 1. Spillets idé og formål

**What experience do I want to create?**

- A game about adaptation rather than memorization
- The satisfaction of solving a unique puzzle every game
- "Aha!" moments when players discover rule interactions
- Relaxed experimentation without fear of long-term consequences
- The feeling of mastery that comes from strategic flexibility

**Core Design Philosophy:**

- Replayability through randomness with depth
- Accessibility through clarity, not simplicity
- Challenge without stress or punishment

---

### 🕹️ 2. Valg for spilleren

**What choices does the player make?**

- **Before Each Turn:** Which card(s) to play from hand
- **Strategic:** When to pursue aggressive vs. defensive play
- **Risk Management:** Whether to use special actions now or save them
- **Adaptation:** How quickly to shift strategy when initial approach fails
- **Meta:** Which difficulty to play at, when to start a new game

**Why these choices matter:**

- Every card play affects future options
- Rules create multiple paths to victory
- Good choices feel smart, bad choices are learning opportunities
- No single "correct" answer due to random elements

---

### ⚖️ 3. Udfordring og balance

**How do I keep the game engaging but not frustrating?**

**Difficulty Balancing:**

- Rules are revealed upfront (no hidden information to memorize)
- Starting games use simpler rule combinations
- AI difficulty can be adjusted
- Games are short, so failure isn't punishing

**Fair Challenge:**

- Player and AI follow identical rules
- Random card draws are neutral (both players face same variance)
- Complex rule combinations appear gradually
- Some rule combinations are hand-crafted to avoid unfun scenarios

**Preventing Frustration:**

- Clear visual feedback shows why a move is illegal
- Tutorial phase teaches core concepts
- Can restart quickly if a game feels unwinnable
- Statistics show overall improvement, not just individual losses

---

### 🔁 4. Gentagelse og rytme

**What makes the game fun to repeat?**

**Short-term Loop (Single Game):**

- Quick turns keep momentum
- New information each turn (cards drawn, opponent's plays)
- Building toward clear goal
- Games end before becoming tedious (3-10 minutes)

**Medium-term Loop (Session):**

- Each new game has different rules = fresh puzzle
- Victories feel earned, defeats feel like learning
- "Just one more game" factor from wanting to try new rules
- No lengthy setup or commitment needed

**Long-term Loop (Many Sessions):**

- Gradually encountering new rule combinations
- Mastering understanding of rule interactions
- Improving win rate against harder AI
- Discovering favorite rule types and strategies

**Variety Within Repetition:**

- ~20-50 different rules = thousands of combinations
- Same rules feel different with different card draws
- AI adapts, preventing pattern exploitation

---

### 💬 5. Feedback til spilleren

**How does the game show success and failure?**

**Immediate Feedback:**

- Cards glow green when playable, gray when not
- Sound effect confirms valid plays
- Gentle rejection animation for invalid moves
- Rule panel highlights relevant rules as they're triggered

**Progress Feedback:**

- Win condition tracker updates in real-time
- Visual indicators show you're close to victory/defeat
- Opponent's progress is visible for strategic planning

**Result Feedback:**

- Victory screen celebrates win with active rules summary
- Defeat screen explains what opponent achieved
- Post-game stats show interesting moments (longest combo, key turning point)

**Learning Feedback:**

- Tooltips explain rule details on hover
- After game, can review rule combinations
- Statistics track improvement over time

---

### 🌍 6. Tema og stemning

**How should the game feel?**

**Atmosphere:**

- Warm and nostalgic (worn table, aged cards, soft lighting)
- Thoughtful and contemplative (not rushed or frantic)
- Slightly mysterious (what rules will appear?)
- Cozy and intimate (like playing cards with a friend)

**Why this theme?**

- Supports the "incomplete games" concept
- Makes random rules feel natural (mixing rule sets from different games)
- Creates safe space for experimentation
- Contrasts with the strategic depth (cozy wrapper around complex system)

**Inspired by:**

- *Inscryption*: aged aesthetic, mysterious card games
- *Slay the Spire*: roguelike randomness with strategic depth
- Traditional card games: timeless, accessible feel

---

### 🧱 7. Regler og begrænsninger

**What core rules govern all games?**

- The core rule of the game is that you play until you've met the end condition, based on the rules of the current game.
- Both player and AI have the same info, and follow the same conditions.

---

### 🚀 8. Motivation og belønning

**What keeps players coming back?**

**Intrinsic Rewards:**

- Satisfaction of solving unique puzzle each game
- Improvement in adaptation skills
- "Aha!" moments discovering rule synergies
- Mastery feeling when winning with unusual rules

**Extrinsic Rewards:**

- Win/loss statistics showing improvement
- Unlocking new rules through play
- Daily challenge completion
- Achievement for rare combinations

**Short-term Motivation:**

- Each game is quick (low commitment)
- Different rules each time (high variety)
- Clear goals during play

**Long-term Motivation:**

- Gradual mastery of strategic flexibility
- Discovering all possible rules
- Improving against harder AI
- Personal challenges (win with specific rule types)

**Avoiding Negative Motivation:**

- No punishment for losses
- No grinding required
- No FOMO (daily challenges are optional)
- No competitive pressure (single-player)

---

### 🧩 9. Udvikling og ændringer

**How has the game evolved?**

- I had originally conisdered multiplayer, but decided to focus on single-player as multiplayer can be challenging to
  make nice.
- I also considered adding a tutorial phase, but decided to focus on the core gameplay instead.
- The rule system was changed to make it easier to implement, but it's not as scalable as I would've liked, as there
  were time constraints.
- To develop further, I'd suggest remaking the AI and rule system, so that it's much more scalable, than the current
  implementation.

---

### 💭 10. Hvad har du lært om spil-design?

**What have I discovered during development?**

- I've learned that making a random rule setup, is difficult, because you can't really think about balance as much,
  because if you balance everything out based on the rules, then the randomness feels unnecessary.
- I've also learned that if you don't have full control of how a game is going to play, then the player can feel
  overwelmed and frustrated.

---

## 11 Inspiration og Referenser

**Primary Inspirations:**

**Inscryption**

- Aged, mysterious aesthetic
- Card game with meta-layers
- Making card games feel fresh and strange
- *What I'm taking:* Visual style, atmospheric presentation
- *What I'm changing:* Removing horror elements, focusing on strategic adaptation

**Slay the Spire**

- Roguelike randomness with strategic depth
- Learning through repeated play with variety
- Clear UI for complex systems
- *What I'm taking:* Philosophy of "random but fair," replayability through variation
- *What I'm changing:* Not deck-building, simpler core mechanics

**Traditional Card Games (Hearts, Spades, Gin Rummy)**

- Clear, timeless rules
- Turn-based strategy with hidden information
- Social and accessible
- *What I'm taking:* Clarity, approachability, classical card game feel
- *What I'm changing:* Making rules variable and unknown until game starts

**Calvinball (Calvin & Hobbes)**

- Rules are made up as you go
- Chaos with structure
- *What I'm taking:* Philosophy of changing rules, embracing uncertainty
- *What I'm changing:* Rules are consistent once established, not arbitrary

---

## 12 Teknisk note

**Development Environment:**

- **Unity Version:** 6000.2.9f1
- **Scripting Language:** C# 9.0
- **Target Framework:** .NET Framework 4.7.1
- **Render Pipeline:** Universal Render Pipeline (URP)

**Tools & Software:**

- **IDE:** JetBrains Rider
- **Version Control:** Git

**Planned Unity Features:**

- UI Toolkit or Unity UI for card interface
- Animation system for card movements
- ScriptableObjects for rule definitions
- JSON for save data and statistics

**Technical Considerations:**

- Modular rule system for easy addition of new rules
- AI decision-making system that can adapt to any rule combination
- Save system for statistics and unlocks
- Efficient card rendering for smooth animations

---

*Document Version: 1.0*  
*Last Updated: 2025-11-06*  
*Status: Alpha*
