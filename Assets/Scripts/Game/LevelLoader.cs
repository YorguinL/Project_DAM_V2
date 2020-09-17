using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    int scene = 5;
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}