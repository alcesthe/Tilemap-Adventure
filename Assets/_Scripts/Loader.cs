using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    public enum Scene
    {
        Start,
        Loading,
        Level_1,
        Level_2,
        Level_3,
        Level_4,
    }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene)
    {
        // Set the loader callback action load the target
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        // Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    private static IEnumerator LoadSceneAsyncIndex(int index)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(index);
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        } else
        {
            return 0f;
        }
    }


    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Get current scene
        // Set the loader callback action load the target
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            if (SceneManager.sceneCountInBuildSettings <= currentSceneIndex + 1)
            {
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(Scene.Start));
            }
            else
            {
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsyncIndex(currentSceneIndex + 1));
            }
        };

        // Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadExit()
    {
        Application.Quit();
    }

    public static void LoaderCallback()
    {
        // Trigger after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
