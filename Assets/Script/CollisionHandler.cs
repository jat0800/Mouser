using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning typically set in the editor
    // CACHE - e.g. reference for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip Fail;
    [SerializeField] AudioClip Win;
    [SerializeField] ParticleSystem FailParticle;
    [SerializeField] ParticleSystem WinParticle;

    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update ()
    {
        RespondToDebug();
    }
    void RespondToDebug ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggle collision
        }
    }
    void OnCollisionEnter (Collision other)
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit Friendly object!");
                break;
            case "Finish":
                StartNextSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }
    void StartCrashSequence () 
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Fail);
        FailParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }
    void ReloadLevel ()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
    void StartNextSequence ()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Win);
        WinParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    void LoadNextLevel ()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
