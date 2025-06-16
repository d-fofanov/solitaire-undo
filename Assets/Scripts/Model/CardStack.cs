using System;
using System.Collections.Generic;

namespace SolitaireTest
{
    public class CardStack : IReadOnlyCardStack
    {
        internal readonly List<Card> _cards = new(); // made internal to allow shallow copies if needed

        public Card Top => _cards.Count > 0 ? _cards[^1] : null;
        public Card Bottom => _cards.Count > 0 ? _cards[0] : null;
        public int Count => _cards.Count;

        public bool CanPush(Card card) => CardPlacementRules.CanPlaceOnTop(Top, card);
        
        public void Push(Card card)
        {
            if (CanPush(card))
                _cards.Add(card);
            else
                throw new InvalidOperationException($"Cannot place {card} on top of {Top}");
        }

        public Card Pop()
        {
            if (_cards.Count == 0) throw new InvalidOperationException("Stack is empty");
            var card = _cards[^1];
            _cards.RemoveAt(_cards.Count - 1);
            return card;
        }

        public IEnumerable<(Card card, int index)> Enumerate()
        {
            for (int i = 0; i < _cards.Count; i++)
                yield return (_cards[i], i);
        }

        public CardStack SplitAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var splitCards = _cards.GetRange(index, _cards.Count - index);
            _cards.RemoveRange(index, _cards.Count - index);

            var newStack = new CardStack();
            newStack._cards.AddRange(splitCards);
            return newStack;
        }

        public bool TryMerge(CardStack other)
        {
            if (other.Count == 0) return false;
            if (!CanPush(other._cards[0])) return false;

            _cards.AddRange(other._cards);
            other._cards.Clear();
            return true;
        }
        
        public bool TrySplitAndMerge(int splitIndex, CardStack target, out CardStack moved)
        {
            moved = null;

            if (splitIndex < 0 || splitIndex >= _cards.Count)
                return false;

            var firstCardToMove = _cards[splitIndex];

            if (!target.CanPush(firstCardToMove))
                return false;

            // CanPush passed, proceed with actual split and merge
            moved = new CardStack();
            moved._cards.AddRange(_cards.GetRange(splitIndex, _cards.Count - splitIndex));

            _cards.RemoveRange(splitIndex, _cards.Count - splitIndex);
            target._cards.AddRange(moved._cards);
            return true;
        }

        public CardStack Clone()
        {
            var clone = new CardStack();
            clone._cards.AddRange(_cards);
            return clone;
        }
    }
}