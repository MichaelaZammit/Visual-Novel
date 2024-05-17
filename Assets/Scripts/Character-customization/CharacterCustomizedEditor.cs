using UnityEngine;
using UnityEditor;

// Custom editor for CharacterCustomizer component
[CustomEditor(typeof(CustomizableCharacter))]
public class CharacterCustomizerEditor : Editor
{
    // Called when the Scene view is rendered
    void OnSceneGUI()
    {
        // Reference to the target CharacterCustomizer component
        CustomizableCharacter customizer = (CustomizableCharacter)target;

        // Create a position handle for moving the character
        customizer.transform.position = Handles.PositionHandle(customizer.transform.position, Quaternion.identity);

        // Create a rotation handle for rotating the character
        customizer.transform.rotation = Handles.RotationHandle(customizer.transform.rotation, customizer.transform.position);
    }
}