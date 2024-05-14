using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphEditor : EditorWindow
{
    private DialogueGraphView _graphView;
    private string _fileName = "New Narrative";

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
        GenerateMiniMap();
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(()=>RequestDataOperation(true)){text = "Save Data"});
        toolbar.Add(new Button(()=>RequestDataOperation(false)){text = "Load Data"});
        
        rootVisualElement.Add(toolbar);
    }
    
    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap{anchored = true};
        // This will give 10px offset from the left side
        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));
        miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
        _graphView.Add(miniMap);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "Ok");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if(save)
            saveUtility.SaveGraph(_fileName);
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}