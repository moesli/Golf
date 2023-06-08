using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForcedReset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if we have forced a reset ...
        if (Input.GetKeyDown(KeyCode.R)) // Replace "R" with the key you want to use to reset
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}