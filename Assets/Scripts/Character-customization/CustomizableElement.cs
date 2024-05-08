using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomizableElement : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] 
    private List<PositionedSprite> _spriteOptions;

    [field:SerializeField]
    public int SpriteIndex { get; private set; }
    
    [SerializeField]
    private List<Color> _colorOptions ;

    [field: SerializeField] 
    public int ColorIndex;

    [ContextMenu("Next Sprite")]
    public PositionedSprite NextSprite()
    {
        SpriteIndex = Mathf.Min(SpriteIndex + 1,_spriteOptions.Count -1);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    [ContextMenu("Previous Sprite")]
    public PositionedSprite PreviousSprite()
    {
        SpriteIndex = Mathf.Max(SpriteIndex - 1,0);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    [ContextMenu("Next Color")] 
    public Color NextColor()
    {
        ColorIndex = Mathf.Min(ColorIndex + 1,_colorOptions.Count -1);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    [ContextMenu("Previous Color")]
    public Color PreviousColor()
    {
        ColorIndex = Mathf.Max(ColorIndex - 1, 0);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    private void UpdateSprite()
    {
        SpriteIndex = Mathf.Clamp(SpriteIndex, 0, _spriteOptions.Count - 1);
        var positionedSprite = _spriteOptions[SpriteIndex];
        _spriteRenderer.sprite = positionedSprite.Sprite;
        transform.localPosition = positionedSprite.PositionModifier;
    }

    private void UpdateColor()
    {
        _spriteRenderer.color = _colorOptions[ColorIndex];  
    } 
}
