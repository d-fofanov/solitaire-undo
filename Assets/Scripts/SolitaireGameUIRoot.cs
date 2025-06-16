using System.Collections.Generic;
using UnityEngine;

namespace SolitaireTest
{
    public class SolitaireGameUIRoot : MonoBehaviour
    {
        [Header("UI Prefabs & Anchors")]
        [Tooltip("Prefab with a configured CardPool component")]
        public CardPool cardPoolPrefab;

        [Tooltip("One RectTransform per stack in order: 4 empty + 8 populated")]
        public RectTransform[] stackPositions;

        private SolitaireGame _game;
        private CardPool _cardPool;
        private readonly List<CardStackView> _stackViews = new();
        private (IReadOnlyCardStack stack, int index)? _pendingClick = null;

        public void UndoMove()
        {
            _game.UndoMove();
        }

        private void OnStackClicked(IReadOnlyCardStack stack, int cardIndex)
        {
            if (_pendingClick == null)
            {
                _pendingClick = (stack, cardIndex);
                return;
            }

            _game.TryMakeMove(_pendingClick.Value.stack, stack, _pendingClick.Value.index);
            _pendingClick = null;
        }

        private void Start()
        {
            if (cardPoolPrefab == null || stackPositions == null || stackPositions.Length != 12)
            {
                Debug.LogError("SolitaireGameUIRoot is not correctly configured.");
                return;
            }

            InitializeCardPool();
            InitializeGame();
            CreateAndBindStackViews();
        }

        private void InitializeCardPool()
        {
            _cardPool = Instantiate(cardPoolPrefab, transform);
        }

        private void InitializeGame()
        {
            _game = new SolitaireGame();
        }

        private void CreateAndBindStackViews()
        {
            for (int i = 0; i < stackPositions.Length; i++)
            {
                var anchor = stackPositions[i];

                var stackViewGO = new GameObject($"CardStackView_{i}", typeof(RectTransform));
                stackViewGO.transform.SetParent(anchor, worldPositionStays: false);

                var stackView = stackViewGO.AddComponent<CardStackView>();
                stackView.Bind(_game.Stacks[i], _cardPool, OnStackClicked);

                _stackViews.Add(stackView);
            }
        }
    }
}