using System;
//using Unity.VisualScripting;
using UnityEngine;

// Serializable class representing a sprite positioned at a specific location
[Serializable]
public class PositionedSprite
{
    // Sprite property of the PositionedSprite
    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    // PositionModifier property of the PositionedSprite
    [field: SerializeField]
    public Vector3 PositionModifier { get; set; }
}