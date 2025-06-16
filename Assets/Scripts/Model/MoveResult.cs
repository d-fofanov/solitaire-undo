namespace SolitaireTest
{
    public class MoveResult
    {
        public IReadOnlyCardStack SourceBefore { get; }
        public IReadOnlyCardStack SourceAfter { get; }
        public IReadOnlyCardStack TargetBefore { get; }
        public IReadOnlyCardStack TargetAfter { get; }
        public IReadOnlyCardStack MovedCards { get; }

        public MoveResult(
            IReadOnlyCardStack sourceBefore,
            IReadOnlyCardStack sourceAfter,
            IReadOnlyCardStack targetBefore,
            IReadOnlyCardStack targetAfter,
            IReadOnlyCardStack movedCards)
        {
            SourceBefore = sourceBefore;
            SourceAfter = sourceAfter;
            TargetBefore = targetBefore;
            TargetAfter = targetAfter;
            MovedCards = movedCards;
        }
    }
}