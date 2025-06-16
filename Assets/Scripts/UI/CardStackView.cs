using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SolitaireTest
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class CardStackView : MonoBehaviour
    {
        private CardPool _cardPool;
        private IReadOnlyCardStack _stack;
        private readonly List<CardView> _views = new();
        private int _lastCardCount = -1;

        /// <summary>
        /// Binds the visual to a specific stack and card pool.
        /// </summary>
        public void Bind(IReadOnlyCardStack stack, CardPool cardPool)
        {
            _stack = stack;
            _cardPool = cardPool;
            ForceUpdateView();
        }

        private void Update()
        {
            if (_stack == null) return;

            if (_stack.Count != _lastCardCount)
            {
                ForceUpdateView();
            }
        }

        private void ForceUpdateView()
        {
            ClearVisuals();

            if (_stack == null || _cardPool == null)
            {
                _lastCardCount = -1;
                return;
            }

            foreach (var (card, _) in _stack.Enumerate())
            {
                var view = _cardPool.Get(transform);
                view.SetCard(card, faceUp: true);
                view.transform.localPosition = Vector3.zero;
                _views.Add(view);
            }

            _lastCardCount = _stack.Count;
        }

        private void ClearVisuals()
        {
            if (_cardPool == null) return;

            foreach (var view in _views)
            {
                _cardPool.Return(view);
            }

            _views.Clear();
        }
    }
}