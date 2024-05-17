using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

namespace Subtegral.DialogueSystem.Runtime
{
    public class DialogueParser : MonoBehaviour
    {
        [SerializeField] private DialogueContainer dialogue;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button choicePrefab;
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private Image backGround1;
        [SerializeField] private Image backGround2;
        [SerializeField] private Image backGround3;
        [SerializeField] private Image backGround4;
        [SerializeField] private Image backGround5;

        private void Start()
        {
            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(narrativeData.TargetNodeGUID);
        }

        private void ProceedToNarrative(string narrativeDataGUID)
        {
            var backGround = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).BackgroundNumber;
            ChangeBackground(backGround);
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            dialogueText.text = ProcessProperties(text);
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            foreach (var choice in choices)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
            }
        }

        private string ProcessProperties(string text)
        {
            foreach (var exposedProperty in dialogue.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }
        private int ChangeBackground(int backNum)
        {

            if (backNum == 0)
            {
                backGround1.enabled = true;
                backGround2.enabled = false;
                backGround3.enabled = false;
                backGround4.enabled = false;
                backGround5.enabled = false;
                Debug.Log("number 1");
            }
            else if (backNum == 1)
            {
                backGround1.enabled = false;
                backGround2.enabled = true;
                backGround3.enabled = false;
                backGround4.enabled = false;
                backGround5.enabled = false;
                Debug.Log("number 2");
            }
            else if (backNum == 2)
            {
                backGround1.enabled = false;
                backGround2.enabled = false;
                backGround3.enabled = true;
                backGround4.enabled = false;
                backGround5.enabled = false;
                Debug.Log("number 3");
            }
            else if (backNum == 3)
            {
                backGround1.enabled = false;
                backGround2.enabled = false;
                backGround3.enabled = false;
                backGround4.enabled = true;
                backGround5.enabled = false;
                Debug.Log("number 4");
            }
            else if (backNum == 4)
            {
                backGround1.enabled = false;
                backGround2.enabled = false;
                backGround3.enabled = false;
                backGround4.enabled = false;
                backGround5.enabled = true;
                Debug.Log("number 5");
            }
            return backNum;
        }
    }
}