using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviour for updating UI elements based on customization state
public class UI_CustomizationPicker : MonoBehaviour
{
    // Reference to the CustomizableElement being controlled
    [SerializeField] private CustomizableElement _customizableElement;

    // References to UI buttons for sprite selection
    [SerializeField] private Button _previousSpriteButton;
    [SerializeField] private Button _nextSpriteButton;

    // References to UI buttons for color selection
    [SerializeField] private Button _previousColorButton;
    [SerializeField] private Button _nextColorButton;

    // Reference to UI text for displaying sprite index
    [SerializeField] private TMP_Text _spriteId;

    // Reference to UI image for displaying color icon
    [SerializeField] private Image _colorIcon;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Update UI elements with initial customization state
        UpdateSpriteId();
        UpdateColorIcon();

        // Add listeners to UI buttons for sprite selection
        _previousSpriteButton.onClick.AddListener(() =>
        {
            _customizableElement.PreviousSprite(); // Select previous sprite
            UpdateSpriteId(); // Update UI with new sprite index
        });
        _nextSpriteButton.onClick.AddListener(() =>
        {
            _customizableElement.NextSprite(); // Select next sprite
            UpdateSpriteId(); // Update UI with new sprite index
        });

        // Add listeners to UI buttons for color selection
        _previousColorButton.onClick.AddListener(() =>
        {
            _customizableElement.PreviousColor(); // Select previous color
            UpdateColorIcon(); // Update UI with new color
        });
        _nextColorButton.onClick.AddListener(() =>
        {
            _customizableElement.NextColor(); // Select next color
            UpdateColorIcon(); // Update UI with new color
        });
    }

    // Method to update UI with current sprite index
    public void UpdateSpriteId()
    {
        _spriteId.SetText(_customizableElement.SpriteIndex.ToString().PadLeft(2, '0')); // Update text with sprite index
    }

    // Method to update UI with current color icon
    public void UpdateColorIcon()
    {
        _colorIcon.color = _customizableElement.CurrentColor; // Update color of the color icon
    }
}
