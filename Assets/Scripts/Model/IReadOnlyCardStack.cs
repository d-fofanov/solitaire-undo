using System.Collections.Generic;

namespace SolitaireTest
{
    public interface IReadOnlyCardStack
    {
        Card Top { get; }
        Card Bottom { get; }
        int Count { get; }

        bool CanPush(Card card);
        IEnumerable<(Card card, int index)> Enumerate();
    }
}