using System.Collections;
using System.Collections.Generic;
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
        //Update Sprites
        return _spriteOptions[SpriteIndex];
    }

    [ContextMenu("Previous Sprite")]
    public PositionedSprite PreviousSprite()
    {
        SpriteIndex = Mathf.Max(SpriteIndex - 1,0);
        //Update Sprites
        return _spriteOptions[SpriteIndex];
    }

    private void UpdateSprite()
    {
        SpriteIndex = Mathf.Clamp(SpriteIndex, 0, _spriteOptions.Count - 1);
        var positionedSprite = _spriteOptions[SpriteIndex];
        _spriteRenderer.sprite = positionedSprite.Sprite;
        transform.localPosition = positionedSprite.PositionModifier;
    }
}
