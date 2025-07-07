using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    static MusicHandler instance;
 
     // Drag in the .mp3 files here, in the editor
    public AudioClip menuMusic;
    public AudioClip lobbyMusic;
    public AudioClip levelOneMusic;
 
    // public AudioSource Audio;
 
    // Singelton to keep instance alive through all scenes
    void Awake()
     {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
 
        DontDestroyOnLoad(gameObject);
 
        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    // Called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Replacement variable (doesn't change the original audio source)
        // AudioSource source = new AudioSource();
        AudioSource source = GetComponent<AudioSource>();

        // Plays different music in different scenes
        switch (scene.name)
        {
            case "MainMenu":
                source.clip = menuMusic;
                break;
            case "Level 1":
                source.clip = levelOneMusic;
                break;
            case "Lobby":
                source.clip = lobbyMusic;
                break;
            default:
                source.clip = null; // Don't play any music otherwise
                break;
        }

        source.enabled = false;
        source.enabled = true;

    }
}
