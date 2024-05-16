using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public TextBoxController bottomBar;
    public BackgroundController backgroundController; // Assuming you have a BackgroundController script

    void Start()
    {
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.Screen_1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    bottomBar.PlayScene(currentScene);
                    backgroundController.SetImage(currentScene.Screen_1);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }
}