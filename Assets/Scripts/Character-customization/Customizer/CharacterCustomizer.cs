using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour responsible for character customization
public class CharacterCustomizer : MonoBehaviour
{
    // Inner class representing a customized character as a ScriptableObject
    [CreateAssetMenu]
    public class CustomizedCharacter : ScriptableObject
    {
        // List of customization data for the character
        [field: SerializeField] public List<CustomizationData> Data { get; private set; }
    }
}