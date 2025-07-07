using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playButton ()
    {
        scene = "Lobby";
        SceneManager.LoadScene(scene);
    }

    public void exitButton ()
    {
        Application.Quit();
    }
}
