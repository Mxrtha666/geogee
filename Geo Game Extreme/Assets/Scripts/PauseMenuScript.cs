using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Stop();
        }
    }
    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale= 0f;
        Paused = true;
    }
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale= 1f;
        Paused = false;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}
