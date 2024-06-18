/*
* Author: Rylan Neo
* Date of creation: 15th June 2024
* Description: Loading screen display script
*/
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public void LoadLevel(int sceneindex)
    {
        StartCoroutine(LoadAsynchronsly(sceneindex));
    }
    private void Awake()
    {
        loadingScreen.SetActive(false);
    }
    IEnumerator LoadAsynchronsly(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        loadingScreen.SetActive(true);
        while (!operation.isDone) 
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
