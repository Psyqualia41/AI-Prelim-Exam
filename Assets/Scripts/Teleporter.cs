using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    string scene;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < 2.5) {
            scene = "Loading 1";
            SceneManager.LoadScene(scene);
        }
    }
}
