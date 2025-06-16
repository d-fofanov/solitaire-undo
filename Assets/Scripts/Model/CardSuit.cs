namespace SolitaireTest
{
    public enum CardSuit
    {
        Spades,
        Diamonds,
        Hearts,
        Clubs,
    }

    public static class CardSuitUtils
    {
        public static bool IsRed(this CardSuit suit) => suit is CardSuit.Hearts or CardSuit.Diamonds;
        public static bool IsBlack(this CardSuit suit) => suit is CardSuit.Spades or CardSuit.Clubs;
    }
}
