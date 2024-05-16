using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogueContainer _dialogueContainer;
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
    private List<Group> CommentBlocks => _targetGraphView.graphElements.ToList().Where(x => x is Group).Cast<Group>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    // public void SaveGraph(string fileName)
    // {
    //     var dialogueContainerObject = ScriptableObject.CreateInstance<DialogueContainer>();
    //     if (!SaveNodes(fileName, dialogueContainerObject)) return;
    //
    //     if (!AssetDatabase.IsValidFolder("Assets/Resources"))
    //         AssetDatabase.CreateFolder("Assets", "Resources");
    //
    //     UnityEngine.Object loadedAsset =
    //         AssetDatabase.LoadAssetAtPath($"Assets/Resources/{fileName}.asset", typeof(DialogueContainer));
    //
    //     if (loadedAsset == null || !AssetDatabase.Contains(loadedAsset))
    //     {
    //         AssetDatabase.CreateAsset(dialogueContainerObject, $"Assets/Resources/{fileName}.asset");
    //     }
    //     else
    //     {
    //         DialogueContainer container = loadedAsset as DialogueContainer;
    //         container.NodeLinks = dialogueContainerObject.NodeLinks;
    //         container.DialogueNodeData = dialogueContainerObject.DialogueNodeData;
    //         EditorUtility.SetDirty(container);
    //     }
    //     
    //     AssetDatabase.SaveAssets();
    // }
    
    public void SaveGraph(string fileName)
    {
        // If there are no edges (no connections) then return
        if (!Edges.Any()) return;

        var dialogueContainerObject = ScriptableObject.CreateInstance<DialogueContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;
            
            dialogueContainerObject.NodeLinks.Add(new NodeLinkData()
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogueContainerObject.DialogueNodeData.Add(new DialogueNodeData()
            {
                Guid = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position
            });
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        
        AssetDatabase.CreateAsset(dialogueContainerObject, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool SaveNodes(string fileName, DialogueContainer dialogueContainerObject)
        {
            if (!Edges.Any()) return false;
            var connectedSockets = Edges.Where(x => x.input.node != null).ToArray();
            for (var i = 0; i < connectedSockets.Count(); i++)
            {
                var outputNode = (connectedSockets[i].output.node as DialogueNode);
                var inputNode = (connectedSockets[i].input.node as DialogueNode);
                dialogueContainerObject.NodeLinks.Add(new NodeLinkData
                {
                    BaseNodeGUID = outputNode.GUID,
                    PortName = connectedSockets[i].output.portName,
                    TargetNodeGuid = inputNode.GUID
                });
            }

            foreach (var node in Nodes.Where(node => !node.EntryPoint))
            {
                dialogueContainerObject.DialogueNodeData.Add(new DialogueNodeData
                {
                    Guid = node.GUID,
                    DialogueText = node.DialogueText,
                    Position = node.GetPosition().position
                });
            }

            return true;
        }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not Found", "Target dialogue graph file does not exist!", "Ok");
            return;
        }

        _dialogueContainer = _containerCache;

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    
    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            var k = i; //Prevent access to modified closure
            Debug.Log(_dialogueContainer);
            Debug.Log(_dialogueContainer.NodeLinks);
            var connections = _dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[k].GUID).ToList();
            for (var j = 0; j < connections.Count(); j++)
            {
                var targetNodeGUID = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);
    
                targetNode.SetPosition(new Rect(
                    _dialogueContainer.DialogueNodeData.First(x => x.Guid == targetNodeGUID).Position,
                    _targetGraphView.defaultNodeSize));
            }
        }
    }
    

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };

        if (tempEdge != null)
        {
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);

            _targetGraphView.Add(tempEdge);
        }
    }
    
    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodeData)
        {
            // We pass position later on, so we can just use Vector2 Zero for now as position without nodes
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText, Vector2.zero);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);
            
            var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGUID == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ClearGraph()
    {
        // Set Entry Points GUID back from the save
        // Discard existing GUID
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGUID;

        foreach (var node in Nodes)
        {
            // Remove Edges that connected to this node
            if (node.EntryPoint) continue;
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));

            // Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }
}
