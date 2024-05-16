using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CustomizableElement customizableElement;

    private void Start()
    {
        CheckForMissingElements();
    }

    private void CheckForMissingElements()
    {
        if (customizableElement == null)
        {
            Debug.LogError("CustomizableElement reference is missing in GameManager!");
            return;
        }

        // Check for missing CustomizationType
        if (customizableElement.requiredCustomizationTypes == null || customizableElement.requiredCustomizationTypes.Length == 0)
        {
            Debug.LogWarning("No CustomizationType assigned to CustomizableElement in GameManager!");
        }

        // Check for missing SpriteRenderer
        if (customizableElement.GetComponent<SpriteRenderer>() == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing in CustomizableElement in GameManager!");
        }

        // Call the method in CustomizableElement to check for missing customization types
        customizableElement.CheckForMissingCustomizationTypes();
    }
}
