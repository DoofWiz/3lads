using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayNextLevel = 1f; //A variable that controls the delay before next level happens.
    [SerializeField]AudioClip crashSound;
    [SerializeField]AudioClip winSound;

    [SerializeField]ParticleSystem crashParticles;
    [SerializeField]ParticleSystem winParticles;

    bool collisionDisabled = false;
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugTrigger();
        //A better way of doing this would be to have something like 'DebugKeys' and all debug functions can go in here.
    }
    void OnCollisionEnter(Collision other) //When the rocket hits a collider...
    {//Start a switch statement to determine what the player has hit. Use the tag system for this.
        if (isTransitioning || collisionDisabled) {return;} //return here means ignore the switch statement. || means 'or'.
        
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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        winParticles.Play();
        GetComponent<GameDevTVRocketMove>().enabled = false;
        Invoke("LoadNextLevel", delayNextLevel);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<GameDevTVRocketMove>().enabled = false;
        Invoke("Respawn", delayNextLevel);
    }
    void Respawn()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        LevelLoadLogic();
    }
    void DebugTrigger()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LevelLoadLogic();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void LevelLoadLogic()
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
