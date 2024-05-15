using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomizationRandomizer : MonoBehaviour
{
    [ContextMenu("Randomize All")]
    public void Randomize()
    {
        var elements = GetComponentsInChildren<CustomizableElement>();
        foreach (var element in elements)
        {
            element.Randomize();
        }
    } 
}
