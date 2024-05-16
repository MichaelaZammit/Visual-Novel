using UnityEngine;
using System.Collections.Generic;

public class DialogueInitializer : MonoBehaviour
{
    public DialogueContainer dialogueContainer;

    public Sprite artboard1Sprite;
    public Sprite drinkingSceneSprite;
    public Sprite video1Sprite;
    public Sprite video2Sprite;

    void Start()
    {
        // Initialize Dialogue Nodes
        DialogueContainer.DialogueNode node1 = new DialogueContainer.DialogueNode
        {
            Guid = System.Guid.NewGuid().ToString(),
            DialogueText = "Start",
            ScreenSprite = artboard1Sprite,
            Position = new Vector2(0, 0)
        };

        DialogueContainer.DialogueNode node2 = new DialogueContainer.DialogueNode
        {
            Guid = System.Guid.NewGuid().ToString(),
            DialogueText = "Fine",
            ScreenSprite = phoneSprite,
            Position = new Vector2(0, 100)
        };

        DialogueContainer.DialogueNode node3 = new DialogueContainer.DialogueNode
        {
            Guid = System.Guid.NewGuid().ToString(),
            DialogueText = "I got ready to go to a party. I dressed my nicest mini red dress and thigh-high black boots cause I knew that Damon would be at the party.",
            ScreenSprite = drinkingSceneSprite,
            Position = new Vector2(0, 200)
        };

        DialogueContainer.DialogueNode node4 = new DialogueContainer.DialogueNode
        {
            Guid = System.Guid.NewGuid().ToString(),
            DialogueText = "When I woke up, I went to check my laptop and saw that he was video calling me.",
            ScreenSprite = video_1Sprite,
            Position = new Vector2(0, 300)
        };

        DialogueContainer.DialogueNode node5 = new DialogueContainer.DialogueNode
        {
            Guid = System.Guid.NewGuid().ToString(),
            DialogueText = "He took off his shirt, then his socks, then his pants, and something more seductively. I was blushing while he was stripping.",
            ScreenSprite = video_2Sprite,
            Position = new Vector2(0, 400)
        };

        // Initialize Node Links
        List<DialogueContainer.NodeLink> nodeLinks = new List<DialogueContainer.NodeLink>
        {
            new DialogueContainer.NodeLink { BaseNodeGuid = node1.Guid, PortName = "Next", TargetNodeGuid = node2.Guid },
            new DialogueContainer.NodeLink { BaseNodeGuid = node2.Guid, PortName = "Next", TargetNodeGuid = node3.Guid },
            new DialogueContainer.NodeLink { BaseNodeGuid = node3.Guid, PortName = "Next", TargetNodeGuid = node4.Guid },
            new DialogueContainer.NodeLink { BaseNodeGuid = node4.Guid, PortName = "Next", TargetNodeGuid = node5.Guid }
        };

        // Create the Dialogue Container
        dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        dialogueContainer.DialogueNodeData = new List<DialogueContainer.DialogueNode> { node1, node2, node3, node4, node5 };
        dialogueContainer.NodeLinks = nodeLinks;

        // Save the Dialogue Container asset (Optional)
        // AssetDatabase.CreateAsset(dialogueContainer, "Assets/DialogueContainer.asset");
        // AssetDatabase.SaveAssets();
    }
}
