using System;
using System.Collections.Generic;
using System.Linq;

namespace SolitaireTest
{
    public class SolitaireGame
    {
        private readonly List<CardStack> _stacks = new();
        private readonly CardPlacementHistory _history;

        public IReadOnlyList<IReadOnlyCardStack> Stacks => _stacks;

        public SolitaireGame()
        {
            InitializeStacks();
            _history = new CardPlacementHistory(_stacks);
        }

        private void InitializeStacks()
        {
            _stacks.Clear();

            // 4 empty stacks
            for (int i = 0; i < 4; i++)
                _stacks.Add(new CardStack());

            // Available colors and suit order for alternating colors
            var redSuits = new[] { CardSuit.Hearts, CardSuit.Diamonds };
            var blackSuits = new[] { CardSuit.Spades, CardSuit.Clubs };

            // Create 8 valid stacks with alternating colors, descending order
            for (int stackIndex = 1; stackIndex <= 8; stackIndex++)
            {
                var stack = new CardStack();
                var honor = CardHonor.King;
                bool red = stackIndex % 2 == 0; // alternate first color across stacks

                for (int i = 0; i < stackIndex && (int)honor >= 1; i++)
                {
                    var suit = red ? redSuits[i % redSuits.Length] : blackSuits[i % blackSuits.Length];
                    var card = Card.Get(suit, honor);

                    // Push will always succeed since we control the sequence
                    stack.Push(card);

                    honor = (CardHonor)((int)honor - 1);
                    red = !red;
                }

                _stacks.Add(stack);
            }
        }


        /// <summary>
        /// Attempts to move part of the source stack onto the target.
        /// Returns true if successful, false otherwise.
        /// </summary>
        public bool TryMakeMove(int sourceIndex, int targetIndex, int splitIndex)
        {
            if (!IsValidStackIndex(sourceIndex) || !IsValidStackIndex(targetIndex))
                return false;

            var source = _stacks[sourceIndex];
            var target = _stacks[targetIndex];

            var result = _history.RecordMove(source, target, splitIndex);
            return result != null;
        }

        /// <summary>
        /// Undoes the last move, if any.
        /// Returns true if a move was undone.
        /// </summary>
        public bool UndoMove()
        {
            return _history.UndoMove() != null;
        }

        private bool IsValidStackIndex(int index)
        {
            return index >= 0 && index < _stacks.Count;
        }
    }
}