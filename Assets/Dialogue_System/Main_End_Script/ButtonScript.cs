using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber;
    public void NextScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
