using System;
using System.Linq;
using UnityEngine;

namespace Character_customization
{
    // MonoBehaviour for customizing individual character elements
    public class CustomizedCharacterElement : MonoBehaviour
    {
        // Customization type of this element
        [field: SerializeField]
        public CustomizationType Type { get; private set; }

        // Reference to the CustomizedCharacter containing customization data
        [SerializeField] private CharacterCustomizer.CustomizedCharacter _character;

        // Reference to the SpriteRenderer component of this element
        private SpriteRenderer _spriteRenderer;

        // Called when the script instance is being loaded
        private void Start()
        {
            // Get the SpriteRenderer component attached to this GameObject
            _spriteRenderer = GetComponent<SpriteRenderer>();

            // Find the customization data corresponding to the current Type
            var customization = _character.Data.FirstOrDefault(d => d.Type == Type);

            // If customization data is found, apply it to the element
            if (customization != null)
            {
                _spriteRenderer.color = customization.Color;
                _spriteRenderer.sprite = customization.Sprite.Sprite;
                transform.localPosition = customization.Sprite.PositionModifier;
            }
        }
    }
}
