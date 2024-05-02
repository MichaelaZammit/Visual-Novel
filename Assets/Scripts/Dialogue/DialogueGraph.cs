using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphEditor : EditorWindow
{
    [SerializeField] private DialogueGraphView _graphView;

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraphEditor>();
        window.titleContent =  new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();
        
        var nodeCreateButton = new Button(() =>
        {
            _graphView.CreateNode("Dialogue Node");
        });
        
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);
        
        rootVisualElement.Add(toolbar);
    }
    
    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
    
    
}

