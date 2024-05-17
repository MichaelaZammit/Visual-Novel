using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Add this line to access LINQ extension methods

public class UI_CustomizationUI : MonoBehaviour
{
    // List of UI_CustomizationPicker components
    private List<UI_CustomizationPicker> _pickers;
    
    // Called when the script instance is being loaded
    void Start()
    {
        // Get all UI_CustomizationPicker components in children and convert to a list
        _pickers = GetComponentsInChildren<UI_CustomizationPicker>().ToList(); 
    }

    // Method to update the state of all UI_CustomizationPicker components
    public void UpdatePickersState()
    {
        // Iterate through all pickers and update their state
        _pickers.ForEach(picker =>
        {
            picker.UpdateSpriteId(); // Update sprite ID
            picker.UpdateColorIcon(); // Update color icon
        });
    }
}