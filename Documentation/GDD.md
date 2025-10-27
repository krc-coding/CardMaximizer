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

The satisfaction comes from both mastering individual rule sets and developing the skill to quickly adapt to unknown combinations.

---

## 4 Regler og Mekanikker

**Meta-Rules (Always Apply):**
- At the start of each game, 3-5 random rules are drawn from a rule pool
- Rules are revealed to both player and AI before the first turn
- The player always goes first
- Each player has a hand of cards and draws from their deck
- The game ends when a win condition (defined by the active rules) is met

**Variable Rules (Examples of what might be drawn):**
- **Win Conditions:** "First to play 5 red cards wins" / "Highest total value after 10 rounds wins" / "First to empty their hand wins"
- **Card Play Rules:** "Only play cards in ascending order" / "Must alternate colors" / "Can play 2 cards per turn"
- **Special Actions:** "Discard and draw 3 cards once per game" / "Skip opponent's turn by playing a matching pair"
- **Scoring Modifiers:** "Face cards are worth double" / "Odd numbers score negative"
- **Deck Rules:** "Start with 7 cards" / "Draw 2 cards per turn" / "No reshuffling discard pile"

**Core Mechanics:**
- Turn-based card play
- Hand management
- Strategic decision-making based on active rules
- Adapting strategy mid-game as you learn the rule interactions

💬 *The challenge comes from quickly understanding how the random rules interact and finding the optimal strategy within those constraints.*

---

## 5 Spilverden og Tema

**Setting:**  
The game takes place on a worn wooden table in a cozy game enthusiast's home. The environment feels lived-in and nostalgic, with incomplete board games on shelves in the background, dice scattered around, and warm ambient lighting from a nearby lamp.

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

💬 *The theme supports the gameplay by creating a space where experimentation feels safe and discovery feels rewarding. The "incomplete games" theme justifies why rules are always changing - you're playing with mixed rule sets from different games.*

---

## 6 Figurer og Objekter

**The Player:**
- Represented by their side of the table
- Can view their hand, drag and play cards
- Makes all decisions about card play and special actions
- No special character abilities (to keep focus on adapting to rules)

**The AI Opponent:**
- Sits across the table
- Follows the same rules as the player
- Shows their card count but hides specific cards
- Makes strategic decisions based on the active rules
- Provides a consistent challenge without feeling unfair

**Cards:**
- Standard deck elements (numbers, suits, face cards)
- Each card has clear, readable values
- Visual design shows aging and wear
- Cards animate smoothly when played, drawn, or discarded

**UI Objects:**
- **Rule Display Panel:** Shows the active rules clearly at the side of the screen
- **Hand Area:** Player's cards displayed in an arc at bottom
- **Play Area:** Central zone where cards are played
- **Deck/Discard Piles:** Visible stacks showing remaining cards
- **Turn Indicator:** Shows whose turn it is
- **Win Condition Tracker:** Displays progress toward victory (if applicable to current rules)

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

**Game-to-Game Progression:**
- **Early Games (Tutorial Phase):**
    - 2-3 simple rules that don't interact much
    - Common win conditions (total points, first to X)
    - AI plays conservatively

- **Mid Progression:**
    - 3-4 rules with some interaction
    - More variety in rule combinations
    - AI adapts to complex rules better

- **Advanced Games:**
    - 4-5 rules that create interesting interactions
    - Rare/unusual rules appear more often
    - AI plays optimally

**Within a Single Game:**
- Each game is relatively short (3-10 minutes)
- Tension builds as win conditions approach
- Strategic depth emerges as players understand rule interactions

**Long-term Progression:**
- **Statistics Tracking:** Win rate, favorite rule combinations, total games played
- **Rule Unlocks:** Start with ~20 rules, unlock more (up to 50+) through play
- **Difficulty Settings:** Adjust AI skill level
- **Daily Challenges:** Pre-set rule combinations for consistent challenge

**Progression Philosophy:**
- No grinding or mandatory progression
- Players improve through understanding, not upgrades
- New rules add variety, not power
- Each game stands alone but long-term play reveals mastery

💬 *The progression system rewards adaptation skills rather than memorization, keeping each game fresh while building player competence.*

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

**Universal Constraints:**
- Player always goes first
- Each player starts equal (same deck, same resources)
- Rules are revealed before play begins
- No take-backs (commits to moves)
- Games have clear end conditions

**Why these constraints?**
- Fairness: Both players follow same rules with same information
- Clarity: No hidden information to remember between games
- Focus: Constraints force creative thinking within limits
- Pacing: Games must end in reasonable time

**Variable Rules Philosophy:**
- Rules should be understandable within 10 seconds
- Rules can interact but shouldn't create impossible scenarios
- Some rules are more common (for familiarity), some are rare (for surprise)
- Rules balanced so no single rule dominates all games

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

*(This section to be filled during development - examples below)*

- **Initial Concept:** Considered multiplayer but focused on single-player for better rule variety pacing
- **Rule Revelation:** Originally rules were hidden until triggered, changed to showing all upfront for fairness
- **Game Length:** Adjusted rule combinations to target 5-7 minute games instead of 15+ minutes
- **AI Difficulty:** Added adjustable difficulty after realizing one-size-fits-all AI wasn't engaging for all players
- **Visual Design:** Moved from digital/clean aesthetic to aged/worn aesthetic to better match theme

*(Add notes here as development progresses)*

---

### 💭 10. Hvad har du lært om spil-design?

**What have I discovered during development?**

*(To be filled during development - key lessons)*

**About Random Systems:**
- Randomness needs clear communication to feel fair
- Players accept randomness when both sides face it equally
- "Controlled randomness" (curated rule pools) better than pure chaos

**About Adaptation Gameplay:**
- Players need time to process rules before action
- Visual clarity is more important than visual flash
- Tutorial must teach "how to adapt" not just "these specific rules"

**About Replayability:**
- Variety alone isn't enough - must create meaningful differences
- Short games + high variety = strong "one more game" pull
- Player mastery should be about skills, not memorization

**About Single-Player Card Games:**
- AI needs to feel intelligent but not psychic
- Players need to feel they could have won with better play
- Balance between luck and skill is delicate

*(Add more observations during development)*

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
- **IDE:** JetBrains Rider / Visual Studio
- **Version Control:** Git
- **Art Tools:** *(To be determined - Photoshop/GIMP for card designs)*
- **Audio Tools:** *(To be determined - Audacity for sound effects)*

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
*Last Updated: 2025-10-27*  
*Status: In Pre-Production*
