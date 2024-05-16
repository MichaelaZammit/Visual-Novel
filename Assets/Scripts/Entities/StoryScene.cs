using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite Screen_1;  
    public StoryScene nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
    }

    // Method to get the background sprite
    public Sprite GetBackground()
    {
        return Screen_1;
    }
}