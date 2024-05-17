using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// MonoBehaviour for controlling game elements
public class Control : MonoBehaviour
{
    // Method to reset the character
    public void ResetTheCharacter()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Print a message to indicate the button is working
        print("The button is working.");
    }
}