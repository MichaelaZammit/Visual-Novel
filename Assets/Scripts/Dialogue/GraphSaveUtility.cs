using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;

public class GraphSaveUtility
{
   private DialogueGraphView _targetGraphView;

   private List<Edge> Edges => _targetGraphView.edges.ToList();
   private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
   
   public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
   {
      return new GraphSaveUtility
      {
         _targetGraphView = targetGraphView
      };
   }

   public void SaveGraph(string fileName)
   {
      // If there are no edges (no connections) then return
      if (!Edges.Any()) return;

      var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
      var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
      for (int i = 0; i < connectedPorts.Length; i++)
      {
         
      }
   }

   public void LoadGraph(string fileName)
   {
      
   }
}
