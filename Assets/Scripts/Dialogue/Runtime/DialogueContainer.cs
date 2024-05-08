using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLink = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
    public object NodeLinks { get; set; }
}