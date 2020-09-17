using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool paused = false;
    public GameObject menu;
    public GameObject settings;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        settings.SetActive(false);

    }
    public void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
   
    public void OpenSettings()
    {
        settings.SetActive(true);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void Retry()
    {
        Resume();
        SceneManager.LoadScene(5);
        
    }
    public void Quit()
    {
        Resume();
        SceneManager.LoadScene(0);

    }

}
