using System;
using UnityEngine;

// This class holds data for a specific customization
[Serializable]
public class CustomizationData 
{
    // The type of customization (e.g., hair, clothing)
    [field: SerializeField]
    public CustomizationType Type { get; private set; }
    
    // The sprite representing the customized part
    [field: SerializeField]
    public PositionedSprite Sprite { get; private set; }
    
    // The color applied to the customized part
    [field: SerializeField]
    public Color Color { get; private set; }

    // Constructor to initialize the customization data
    public CustomizationData(CustomizationType t, PositionedSprite s, Color c)
    {
        Type = t;     // Assigns the customization type
        Sprite = s;   // Assigns the positioned sprite
        Color = c;    // Assigns the color
    }
}