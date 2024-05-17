using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CustomizedCharacter Scriptable Object:
[CreateAssetMenu]
public class CustomizedCharacter : ScriptableObject
{
    // Data Property:
    [field: SerializeField] // Serialized for editing in Unity Editor
    public List<CustomizationData> Data { get; private set; }
}
