using System;
using System.Collections.Generic;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueContainer", menuName = "Dialogue/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public List<NodeLink> NodeLinks = new List<NodeLink>();
    public List<DialogueNode> DialogueNodeData = new List<DialogueNode>();
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();

    [Serializable]
    public class NodeLink
    {
        public string BaseNodeGuid;
        public string PortName;
        public string TargetNodeGuid;
    }

    [Serializable]
    public class DialogueNode
    {
        public string Guid;
        public string DialogueText;
        public Sprite ScreenSprite;  // Add a reference to a sprite
        public Vector2 Position;
    }
}

