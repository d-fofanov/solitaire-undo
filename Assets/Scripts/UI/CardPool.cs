using System.Collections.Generic;
using UnityEngine;

namespace SolitaireTest
{
    public class CardPool : MonoBehaviour
    {
        [Tooltip("Assign a prefab with a CardView component and Image inside")]
        public CardView cardViewPrefab;

        private readonly Queue<CardView> _pool = new();

        /// <summary>
        /// Gets a CardView from the pool or instantiates a new one if needed.
        /// </summary>
        public CardView Get(Transform parent = null)
        {
            CardView view = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(cardViewPrefab, parent);
            if (parent != null)
                view.transform.SetParent(parent, worldPositionStays: false);

            view.gameObject.SetActive(true);
            return view;
        }

        /// <summary>
        /// Returns a CardView to the pool and disables it.
        /// </summary>
        public void Return(CardView view)
        {
            view.gameObject.SetActive(false);
            view.transform.SetParent(transform);
            _pool.Enqueue(view);
        }

        /// <summary>
        /// Gets multiple CardViews at once.
        /// </summary>
        public IEnumerable<CardView> GetMany(int count, Transform parent = null)
        {
            for (int i = 0; i < count; i++)
                yield return Get(parent);
        }

        /// <summary>
        /// Returns multiple CardViews at once.
        /// </summary>
        public void ReturnMany(IEnumerable<CardView> views)
        {
            foreach (var view in views)
                Return(view);
        }
    }

}