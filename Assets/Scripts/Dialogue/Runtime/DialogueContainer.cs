    using System;
    using System.Collections.Generic;
    using UnityEngine;

public class DialogueContainer : ScriptableObject

{
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
        public Vector2 Position;
    }

    
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
}
