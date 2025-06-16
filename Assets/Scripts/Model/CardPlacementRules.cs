using System;
using System.Collections.Generic;
using System.Linq;

namespace SolitaireTest
{
    public static class CardPlacementRules
    {
        // Example rule: Only allow placing cards of opposite color and one less in value
        public static bool CanPlaceOnTop(Card topCard, Card newCard)
        {
            if (topCard == null) return newCard.Honor == CardHonor.King; // empty stack
            if (IsSameColor(topCard.Suit, newCard.Suit)) return false;
            return (int)newCard.Honor == (int)topCard.Honor - 1;
        }

        public static IEnumerable<CardStack> GetValidPlacementStacks(Card card, IEnumerable<CardStack> stacks) =>
            stacks.Where(stack => CanPlaceOnTop(stack.Top, card));

        private static bool IsSameColor(CardSuit a, CardSuit b) =>
            (a.IsRed() && b.IsRed()) || (a.IsBlack() && b.IsBlack());
    }
}