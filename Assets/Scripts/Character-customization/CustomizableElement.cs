using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomizableElement : MonoBehaviour
{
    // Required customization types for this element
    [SerializeField] public CustomizationType[] requiredCustomizationTypes;

    // Method to check for missing customization types
    public void CheckForMissingCustomizationTypes()
    {
        bool missing = false;
        foreach (var customizationType in requiredCustomizationTypes)
        {
            if (customizationType == null)
            {
                Debug.LogWarning("Missing scriptable object: " + customizationType.name, this);
                missing = true;
            }
        }

        if (missing)
        {
            Debug.LogWarning("Some scriptable objects are missing from the character. Please ensure all required customization types are assigned.", this);
        }
        else
        {
            Debug.Log("All required scriptable objects are assigned to the character.", this);
        }
    }

    // Current customization type
    [SerializeField] public CustomizationType _type;

    // SpriteRenderer component for displaying the sprite
    [SerializeField] public SpriteRenderer _spriteRenderer;

    // List of sprite options for customization
    [SerializeField] private List<PositionedSprite> _spriteOptions;

    // Current index of the sprite
    [field: SerializeField] public int SpriteIndex { get; private set; }

    // List of color options for customization
    [SerializeField] private List<Color> _colorOptions;

    // Current index of the color
    [field: SerializeField] public int ColorIndex;

    // Property to get the current color
    public Color CurrentColor => _colorOptions.Count == 0 ? Color.white : _colorOptions[ColorIndex];

    // Context menu item to switch to the next sprite
    [ContextMenu("Next Sprite")]
    public PositionedSprite NextSprite()
    {
        SpriteIndex = Mathf.Min(SpriteIndex + 1, _spriteOptions.Count - 1);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    // Context menu item to switch to the previous sprite
    [ContextMenu("Previous Sprite")]
    public PositionedSprite PreviousSprite()
    {
        SpriteIndex = Mathf.Max(SpriteIndex - 1, 0);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    // Context menu item to switch to the next color
    [ContextMenu("Next Color")]
    public Color NextColor()
    {
        ColorIndex = Mathf.Min(ColorIndex + 1, _colorOptions.Count - 1);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    // Context menu item to switch to the previous color
    [ContextMenu("Previous Color")]
    public Color PreviousColor()
    {
        ColorIndex = Mathf.Max(ColorIndex - 1, 0);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    // Context menu item to randomize the sprite and color
    [ContextMenu("Randomize")]
    public void Randomize()
    {
        SpriteIndex = Random.Range(0, _spriteOptions.Count);
        ColorIndex = Random.Range(0, _colorOptions.Count);
        UpdateSprite();
        UpdateColor();
    }

    // Context menu item to update the position modifier of the sprite
    [ContextMenu("Update Position Modifier")]
    public void UpdateSpritePositionModifier()
    {
        _spriteOptions[SpriteIndex].PositionModifier = transform.localPosition;
    }

    // Get the current customization data
    public CustomizationData GetCustomizationData()
    {
        return new CustomizationData(_type, _spriteOptions[SpriteIndex], _spriteRenderer.color);
    }

    // Update the sprite based on the current SpriteIndex
    private void UpdateSprite()
    {
        if (_spriteOptions.Count == 0) return;

        SpriteIndex = Mathf.Clamp(SpriteIndex, 0, _spriteOptions.Count - 1);
        var positionedSprite = _spriteOptions[SpriteIndex];
        _spriteRenderer.sprite = positionedSprite.Sprite;
        transform.localPosition = positionedSprite.PositionModifier;
    }

    // Update the color based on the current ColorIndex
    private void UpdateColor()
    {
        if (_colorOptions.Count == 0) return;
        ColorIndex = Mathf.Clamp(ColorIndex, 0, _colorOptions.Count - 1);
        var newColor = _colorOptions[ColorIndex];
        _spriteRenderer.color = newColor;
    }

    // Enumerator not implemented
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
