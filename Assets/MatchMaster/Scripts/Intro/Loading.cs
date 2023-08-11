using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SNG.Save;

public class Loading : MonoBehaviour
{
    public Image LoadingBar;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingBarAsync());
    }

    IEnumerator LoadingBarAsync()
    {
        if (SaveGame.Instance.GameState.ShouldBeLoaded == true)
        {
            SaveGame.Instance.GameState.ShouldBeLoaded = false;
            // first load the MainMenuScene in the background, then load GameScene
            yield return StartCoroutine(LoadSceneAsync("GameScene"));
        }
        else
        {
            yield return StartCoroutine(LoadSceneAsync("MainMenuScene"));
        }
    }

    IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.fillAmount = progress;
            yield return null;
        }
    }
}