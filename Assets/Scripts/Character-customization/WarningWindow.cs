using UnityEngine;
using UnityEditor;

public class WarningWindow : EditorWindow
{
    private string warningMessage = "";
    private System.Action okAction;
    private System.Action cancelAction;

    public static WarningWindow instance;

    public static void ShowWindow(string message, System.Action okCallback, System.Action cancelCallback)
    {
        instance = GetWindow<WarningWindow>("Warning");
        instance.warningMessage = message;
        instance.okAction = okCallback;
        instance.cancelAction = cancelCallback;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField(warningMessage, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            okAction?.Invoke();
            instance.Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            cancelAction?.Invoke();
            instance.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}