You're an expert unity developer with a lot of experience creating card games. You speak and understand English, write high-quality modular and reusable code without any bugs.

---

You're creating a Solitaire-like game and work on the card placement system including the following entities:
- gaming card
- card's "place on the table"
- cards stack
- card placement rules
- card placement history.

Write c# code for 
- card's suits and honors enums
- card's class with all card's instances precreated and available through static properties and enumeration methods by suit and honor

---

Great!
Next write the card stack and card placement rules classes:
- card stack allows to find out what card is on the top&bottom of the stack and to iterate stack providing card&index pairs
- has methods for splitting the stack on specified card or index, merging stacks 
- stack merging uses card placement rules to check if the merge is possible

Card placement rules is a static class with methods:
- if a card can be placed on top of a stack
- on what stacks from enumeration can specified card be placed

---

Very well.
Write card placement history class next, please.
It should
- require initial CardStacks enumeration in constructor
- has RecordMove method that provides source and target stacks together with a split index on the source stack to specify transferred cards
- has UndoMove method that removes last recorded move from the history
- both methods return a new instance of the MoveResult class or null on failure

MoveResult has
- IReadOnlyCardStack for source&target stacks in two versions: before and after the move
- IReadOnlyCardStack for the moved stack

IReadOnlyCardStack is an interface containing all the read-only properties and methods of a CardStack

---

That's great.
I'd like CardStack to have a method TrySplitAndMerge to avoid the rollback in RecordMove method. Implement it please.

---

Nice.
I'd like TrySplitAndMerge to avoid tempSplit creation if CanPush returns false. Can you change it so it calls CanPush using card at splitIndex and creates tempSplit collection only on success?


---

Awesome, thank you.
Now I need a temporary SolitaireGame class that
- starts with 12 CardStacks: 4 empty, 8 having correct card stacks of different lengths
- publishes readonly versions of the stacks
- publishes TryMakeMove and UndoMove methods


---

I'd like you to rewrite InitializeStacks method to manually add cards in the correct order so that Push method would be guaranteed to work


---

That's good, thank you.
Now I'll need a UI class for unity GUI representing a card that
- has Sprite[] to be filled with card's sprites in order of suits and honours, 0th element is for the card's back
- by default shows card's back
- allows a Card to be set to update its visual


---

Great.
Write a CardPool class taking CardView prefab and providing methods to get/return one or an enumeration of cards


---

Wonderful.
Now write a CardStackView please: together with IReadOnlyCardStack to visualize it will receive CardPool to use. For simplicity make it check for changes of the stack in Update


---

That's good, but I'd like CardPool to be passed in in Bind not through a serialized field. Update it please.


---


I need an overload to SolitaireGame.TryMakeMove that takes source&target IReadOnlyCardStack + split index. Write it for me please


