using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueGraphEditor : EditorWindow
{
    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraphEditor>();
        window.titleContent =  new GUIContent("Dialogue Graph");
    }
}

