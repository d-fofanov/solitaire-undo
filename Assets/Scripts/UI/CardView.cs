using UnityEngine;
using UnityEngine.UI;

namespace SolitaireTest
{
    public class CardView : MonoBehaviour
    {
        [Tooltip("Index 0 is the card back. Remaining should be ordered by suit * 13 + (honor - 1)")]
        public Sprite[] sprites;

        [SerializeField]
        private Image _image;
        
        private Card _card;
        private bool _isFaceUp = false;

        private void Awake()
        {
            ShowBack(); // default
        }

        /// <summary>
        /// Assigns the card and shows its front or back based on current state.
        /// </summary>
        public void SetCard(Card card, bool faceUp = true)
        {
            _card = card;
            _isFaceUp = faceUp;
            UpdateVisual();
        }

        /// <summary>
        /// Flips the card to face-up or face-down.
        /// </summary>
        public void SetFaceUp(bool faceUp)
        {
            _isFaceUp = faceUp;
            UpdateVisual();
        }

        public void ShowBack() => SetFaceUp(false);
        public void ShowFront() => SetFaceUp(true);

        private void UpdateVisual()
        {
            if (sprites == null || sprites.Length == 0 || _image == null)
            {
                Debug.LogWarning("CardView is not properly configured.");
                return;
            }

            if (!_isFaceUp || _card == null)
            {
                _image.sprite = sprites[0]; // card back
                return;
            }

            int index = 1 + GetCardSpriteIndex(_card);
            if (index >= sprites.Length)
            {
                Debug.LogWarning($"Sprite index out of range for {_card}");
                return;
            }

            _image.sprite = sprites[index];
        }

        private int GetCardSpriteIndex(Card card)
        {
            // Suit order: Clubs (0), Diamonds (1), Hearts (2), Spades (3)
            // Honor order: Ace (1) to King (13)
            return ((int)card.Suit * 13) + ((int)card.Honor - 1);
        }
    }
}