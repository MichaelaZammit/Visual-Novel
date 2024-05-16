using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Subtegral.DialogueSystem.Runtime
{
    public class DialogueParser : MonoBehaviour
    {
        [SerializeField] private DialogueContainer dialogue;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button choicePrefab;
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private StoryScene storyScene;
        [SerializeField] private BackgroundController backgroundController; // Add reference to BackgroundController

        private void Start()
        {
            if (dialogue != null)
            {
                var narrativeData = dialogue.NodeLinks.First(); // Entrypoint node
                ProceedToNarrative(narrativeData.TargetNodeGuid);
            }
            else if (storyScene != null)
            {
                ProceedToStoryScene(storyScene);
            }
        }

        private void ProceedToNarrative(string narrativeDataGUID)
        {
            var nodeData = dialogue.DialogueNodeData.Find(x => x.Guid == narrativeDataGUID);
            dialogueText.text = ProcessProperties(nodeData.DialogueText);
            
            // Set the background image based on the node's ScreenSprite
            if (nodeData.ScreenSprite != null)
            {
                backgroundController.SetImage(nodeData.ScreenSprite);
            }

            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGuid == narrativeDataGUID);
            ClearButtons();

            foreach (var choice in choices)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = ProcessProperties(choice.PortName);
                }
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGuid));
            }
        }

        private void ProceedToStoryScene(StoryScene scene)
        {
            if (scene.sentences.Count > 0)
            {
                dialogueText.text = scene.sentences[0].text;
            }

            ClearButtons();

            if (scene.nextScene != null)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "Next";
                }
                button.onClick.AddListener(() => ProceedToStoryScene(scene.nextScene));
            }
        }

        private string ProcessProperties(string text)
        {
            if (dialogue != null && dialogue.ExposedProperties != null)
            {
                foreach (var exposedProperty in dialogue.ExposedProperties)
                {
                    text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
                }
            }
            return text;
        }

        private void ClearButtons()
        {
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }
        }
    }
}
