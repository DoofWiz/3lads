using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayNextLevel = 1f; //A variable that controls the delay before next level happens.

    void OnCollisionEnter(Collision other) //When the rocket hits a collider...
    {//Start a switch statement to determine what the player has hit. Use the tag system for this.
        switch (other.gameObject.tag)
        {
            case "Friendly": //
                Debug.Log("You hit the: " + gameObject.name);
                break;
            case "Finish":
                StartWinSequence();
                break;
            case "Fuel":
                Debug.Log("You collected some fuel.");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartWinSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", delayNextLevel);
    }

    void StartCrashSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("Respawn", delayNextLevel);
    }
    void Respawn()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
