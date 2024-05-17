using UnityEngine;
using UnityEditor;

// EditorWindow for displaying warning messages
public class WarningWindow : EditorWindow
{
    // Message to display in the window
    private string warningMessage = "";
    // Action to perform when OK button is clicked
    private System.Action okAction;
    // Action to perform when Cancel button is clicked
    private System.Action cancelAction;

    // Static instance of the WarningWindow
    public static WarningWindow instance;

    // Method to show the WarningWindow
    public static void ShowWindow(string message, System.Action okCallback, System.Action cancelCallback)
    {
        // Get or create an instance of WarningWindow
        instance = GetWindow<WarningWindow>("Warning");
        // Set the warning message and callbacks
        instance.warningMessage = message;
        instance.okAction = okCallback;
        instance.cancelAction = cancelCallback;
    }

    // Called to draw the window contents
    private void OnGUI()
    {
        // Display the warning message with a bold label
        EditorGUILayout.LabelField(warningMessage, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Draw OK and Cancel buttons horizontally
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            // Invoke the OK action if it's assigned, then close the window
            okAction?.Invoke();
            instance.Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            // Invoke the Cancel action if it's assigned, then close the window
            cancelAction?.Invoke();
            instance.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}