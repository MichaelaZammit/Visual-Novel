using UnityEngine;
using UnityEditor;

// Custom editor for GameManager component
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    // Called when the script is loaded or a value is changed in the Inspector
    private void OnEnable()
    {
        CheckForMissingElements();
    }

    // Checks for missing references in GameManager and its CustomizableElement
    private void CheckForMissingElements()
    {
        GameManager gameManager = (GameManager)target;

        // Check for missing CustomizableElement reference
        if (gameManager.customizableElement == null)
        {
            ShowWarning("CustomizableElement reference is missing in GameManager!");
            return;
        }

        CustomizableElement customizableElement = gameManager.customizableElement;

        // Check for missing CustomizationType reference
        if (customizableElement._type == null)
        {
            ShowWarning("CustomizationType reference is missing in CustomizableElement!");
        }

        // Check for missing SpriteRenderer component
        if (customizableElement._spriteRenderer == null)
        {
            ShowWarning("SpriteRenderer component is missing in CustomizableElement!");
        }
    }

    // Shows a warning dialog with options
    private void ShowWarning(string message)
    {
        int option = EditorUtility.DisplayDialogComplex("Warning", message, "OK", "Cancel", "Ignore");

        switch (option)
        {
            case 0: // OK
                Debug.Log("User pressed OK");
                break;
            case 1: // Cancel
                Debug.Log("User pressed Cancel");
                break;
            case 2: // Ignore
                Debug.Log("User pressed Ignore");
                break;
        }
    }
}