using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference to a CustomizableElement in the scene
    [SerializeField] public CustomizableElement customizableElement;

    // Called when the script instance is being loaded
    private void Start()
    {
        CheckForMissingElements();
    }

    // Checks for missing elements and logs warnings or errors
    private void CheckForMissingElements()
    {
        // Check if customizableElement is assigned
        if (customizableElement == null)
        {
            Debug.LogError("CustomizableElement reference is missing in GameManager!");
            return;
        }

        // Check if any CustomizationType is assigned
        if (customizableElement.requiredCustomizationTypes == null || customizableElement.requiredCustomizationTypes.Length == 0)
        {
            Debug.LogWarning("No CustomizationType assigned to CustomizableElement in GameManager!");
        }

        // Check if a SpriteRenderer component is attached
        if (customizableElement.GetComponent<SpriteRenderer>() == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing in CustomizableElement in GameManager!");
        }

        // Call the method in CustomizableElement to check for missing customization types
        customizableElement.CheckForMissingCustomizationTypes();
    }
}