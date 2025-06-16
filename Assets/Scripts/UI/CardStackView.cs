using System;
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
        private Action<IReadOnlyCardStack, int> _onClick = null;

        /// <summary>
        /// Binds the visual to a specific stack and card pool.
        /// </summary>
        public void Bind(IReadOnlyCardStack stack, CardPool cardPool, Action<IReadOnlyCardStack, int> onClick = null)
        {
            _stack = stack;
            _cardPool = cardPool;
            _onClick = onClick;
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

            foreach (var (card, idx) in _stack.Enumerate())
            {
                var view = _cardPool.Get(transform);
                view.Setup(card, faceUp: true, onClick: () => OnCardClicked(card, idx));
                view.transform.localPosition = Vector3.zero;
                _views.Add(view);
            }

            _lastCardCount = _stack.Count;
        }

        private void OnCardClicked(Card card, int idx)
        {
            _onClick?.Invoke(_stack, idx);
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