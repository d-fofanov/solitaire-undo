namespace SolitaireTest
{
    public class MoveResult
    {
        public int SourceIdx { get; }
        public int TargetIdx { get; }
        public IReadOnlyCardStack SourceBefore { get; }
        public IReadOnlyCardStack SourceAfter { get; }
        public IReadOnlyCardStack TargetBefore { get; }
        public IReadOnlyCardStack TargetAfter { get; }
        public IReadOnlyCardStack MovedCards { get; }

        public MoveResult(
            int sourceIdx,
            int targetIdx,
            IReadOnlyCardStack sourceBefore,
            IReadOnlyCardStack sourceAfter,
            IReadOnlyCardStack targetBefore,
            IReadOnlyCardStack targetAfter,
            IReadOnlyCardStack movedCards)
        {
            SourceIdx = sourceIdx;
            TargetIdx = targetIdx;
            SourceBefore = sourceBefore;
            SourceAfter = sourceAfter;
            TargetBefore = targetBefore;
            TargetAfter = targetAfter;
            MovedCards = movedCards;
        }
    }
}