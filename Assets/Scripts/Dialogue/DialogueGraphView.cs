using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(200, 150);
    public DialogueNode EntryPointNode;
    private NodeSearchWindow _searchWindow;
    
    public DialogueGraphView(EditorWindow editorWindow)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
        
        AddElement(GenerateEntryPointNode());
        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Init(editorWindow,this);
        nodeCreationRequest = context =>
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    
    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "EntryPoint",
            EntryPoint = true
        };
        
        var generatePort = GeneratePort(node, Direction.Output);
        generatePort.portName = "Next";
        node.outputContainer.Add(generatePort);
        
        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName, Vector2 position)
    {
        AddElement(CreateDialogueNode(nodeName,position));
    }
    
    public DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };
        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);
    
        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
        
        var button = new Button(clickEvent: () =>
        {
            AddChoicePort(dialogueNode);
        });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);
    
        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.DialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });
        
        textField.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(textField);
        
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(position, defaultNodeSize));
        
        return dialogueNode;
    }
    
    public DialogueNode createDialogueNode(string nodeName, Vector2 position)
    {
        var tempDialogueNode = new DialogueNode()
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };
        tempDialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
        var inputPort = GetPortInstance(tempDialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        tempDialogueNode.inputContainer.Add(inputPort);
        tempDialogueNode.RefreshExpandedState();
        tempDialogueNode.RefreshPorts();
        tempDialogueNode.SetPosition(new Rect(position,
            defaultNodeSize)); //To-Do: implement screen center instantiation positioning

        var textField = new TextField("");
        textField.RegisterValueChangedCallback(evt =>
        {
            tempDialogueNode.DialogueText = evt.newValue;
            tempDialogueNode.title = evt.newValue;
        });
        textField.SetValueWithoutNotify(tempDialogueNode.title);
        tempDialogueNode.mainContainer.Add(textField);

        var button = new Button(() => { AddChoicePort(tempDialogueNode); })
        {
            text = "Add Choice"
        };
        tempDialogueNode.titleButtonContainer.Add(button);
        return tempDialogueNode;
    }
    
    private Port GetPortInstance(DialogueNode node, Direction nodeDirection,
        Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
    }


    public void AddChoicePort(DialogueNode dialogueNode, string overriddenPortName = "")
    {
        var generatePort = GetPortInstance(dialogueNode, Direction.Output);

        var oldLabel = generatePort.contentContainer.Q<Label>("type");
        generatePort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogueNode.outputContainer.Query(name: "Connector").ToList().Count;

        var choicePortName =
            string.IsNullOrEmpty(overriddenPortName) ? $"Choice {outputPortCount}" : overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback(evt => generatePort.portName = evt.newValue);
        generatePort.contentContainer.Add(new Label(""));
        generatePort.contentContainer.Add(textField);
        var deleteButton = new Button(()=>RemovePort(dialogueNode, generatePort))
        {
            text = "X"
        };
        generatePort.contentContainer.Add(deleteButton);
        
        generatePort.portName = choicePortName;
        dialogueNode.outputContainer.Add(generatePort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }

    private void RemovePort(DialogueNode dialogueNode, Port generatePort)
    {
        var targetEdge = edges.ToList().Where(x =>x.output.portName == generatePort.portName && x.output.node == generatePort.node);
        
        if(!targetEdge.Any()) return;
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        dialogueNode.outputContainer.Remove(generatePort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}

