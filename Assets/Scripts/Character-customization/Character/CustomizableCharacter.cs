using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{
    // Reference to a scriptable object containing customized character data
    [SerializeField] private CustomizedCharacter _character;

    // Context menu item to randomize all customizable elements
    [ContextMenu("Randomize All")]
    public void Randomize()
    {
        // Get all CustomizableElement components in the children of this GameObject
        var elements = GetComponentsInChildren<CustomizableElement>();

        // Randomize each customizable element
        foreach (var element in elements)
        {
            element.Randomize();
        }
    }

    // Store the current customization information into the _character scriptable object
    public void StoreCustomizationInformation()
    {
        // Get all CustomizableElement components in the children of this GameObject
        var elements = GetComponentsInChildren<CustomizableElement>();

        // Clear existing customization data
        _character.Data.Clear();

        // Add the customization data of each element to the _character scriptable object
        foreach (var element in elements)
        {
            _character.Data.Add(element.GetCustomizationData());
        }
    }
}