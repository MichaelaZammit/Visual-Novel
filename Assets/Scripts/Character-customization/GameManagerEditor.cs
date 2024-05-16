using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private void OnEnable()
    {
        CheckForMissingElements();
    }

    private void CheckForMissingElements()
    {
        GameManager gameManager = (GameManager)target;

        if (gameManager.customizableElement == null)
        {
            ShowWarning("CustomizableElement reference is missing in GameManager!");
            return;
        }

        CustomizableElement customizableElement = gameManager.customizableElement;

        if (customizableElement._type == null)
        {
            ShowWarning("CustomizationType reference is missing in CustomizableElement!");
        }

        if (customizableElement._spriteRenderer == null)
        {
            ShowWarning("SpriteRenderer component is missing in CustomizableElement!");
        }
    }

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