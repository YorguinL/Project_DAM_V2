using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{

    public void ReturnMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Ranking()
    {
        SceneManager.LoadScene(1);

    }
    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(3);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(4);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
