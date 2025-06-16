using System;
using System.Collections.Generic;
using System.Linq;

namespace SolitaireTest
{
    public sealed class Card
    {
        public CardSuit Suit { get; }
        public CardHonor Honor { get; }

        private static readonly Dictionary<(CardSuit, CardHonor), Card> _allCards;

        static Card()
        {
            _allCards = new Dictionary<(CardSuit, CardHonor), Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardHonor honor in Enum.GetValues(typeof(CardHonor)))
                {
                    var card = new Card(suit, honor);
                    _allCards[(suit, honor)] = card;
                }
            }
        }

        private Card(CardSuit suit, CardHonor honor)
        {
            Suit = suit;
            Honor = honor;
        }

        public static Card Get(CardSuit suit, CardHonor honor) => _allCards[(suit, honor)];

        public static IEnumerable<Card> All => _allCards.Values;

        public static IEnumerable<Card> AllOfSuit(CardSuit suit)
            => _allCards.Values.Where(c => c.Suit == suit);

        public static IEnumerable<Card> AllOfHonor(CardHonor honor)
            => _allCards.Values.Where(c => c.Honor == honor);

        public override string ToString() => $"{Honor} of {Suit}";

        private bool Equals(Card other)
        {
            return Suit == other.Suit && Honor == other.Honor;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Card other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Suit, (int)Honor);
        }
    }
}
