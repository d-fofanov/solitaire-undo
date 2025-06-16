using System;
using System.Collections.Generic;

namespace SolitaireTest
{
    public class CardPlacementHistory
    {
        private readonly List<CardStack> _stacks;
        private readonly Stack<MoveResult> _history = new();

        public CardPlacementHistory(IEnumerable<CardStack> initialStacks)
        {
            _stacks = new List<CardStack>(initialStacks);
        }

        public MoveResult RecordMove(CardStack source, CardStack target, int splitIndex)
        {
            var sourceIdx = _stacks.IndexOf(source);
            var targetIdx = _stacks.IndexOf(target);
            
            if (sourceIdx == -1 || targetIdx == -1)
                return null;

            var sourceBefore = source.Clone();
            var targetBefore = target.Clone();

            if (!source.TrySplitAndMerge(splitIndex, target, out var moved))
                return null;

            var result = new MoveResult(
                sourceIdx,
                targetIdx,
                sourceBefore,
                source.Clone(),
                targetBefore,
                target.Clone(),
                moved.Clone()
            );

            _history.Push(result);
            return result;
        }


        public MoveResult UndoMove()
        {
            if (_history.Count == 0) return null;

            var last = _history.Pop();

            if (last == null) return null;

            // Revert both stacks by clearing and restoring from "before" snapshots
            var srcStack = _stacks[last.SourceIdx];
            var tgtStack = _stacks[last.TargetIdx];
            srcStack._cards.Clear();
            srcStack._cards.AddRange(((CardStack)last.SourceBefore)._cards);

            tgtStack._cards.Clear();
            tgtStack._cards.AddRange(((CardStack)last.TargetBefore)._cards);

            return new MoveResult(
                last.TargetIdx,
                last.SourceIdx,
                last.TargetAfter,
                last.TargetBefore,
                last.SourceAfter,
                last.SourceBefore,
                last.MovedCards
            );
        }
    }
}