using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartButtonHandler : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public string[] sceneAddresses; // Array of scene addresses to load
    public string loginSceneName; // Name of the login scene in the build settings

    public void OnStartButtonPressed()
    {
        // Show the loading screen
        loadingScreen.SetActive(true);
        Debug.Log("Loading screen activated");

        // Start loading scenes
        StartCoroutine(LoadScenesInBackground());
    }

    private IEnumerator LoadScenesInBackground()
    {
        float totalProgress = 0f;
        int totalScenes = sceneAddresses.Length;

        for (int i = 0; i < totalScenes; i++)
        {
            Debug.Log($"Starting to load scene {sceneAddresses[i]}");
            var handle = Addressables.LoadSceneAsync(sceneAddresses[i], LoadSceneMode.Additive, false);

            while (!handle.IsDone)
            {
                // Update progress bar
                progressBar.value = (totalProgress + handle.PercentComplete) / totalScenes;
                Debug.Log($"Loading progress for {sceneAddresses[i]}: {handle.PercentComplete}");
                yield return null;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                totalProgress += 1f;
                Debug.Log($"Successfully loaded scene {sceneAddresses[i]}");
            }
            else
            {
                Debug.LogError($"Failed to load scene: {sceneAddresses[i]}");
            }
        }

        // Once all addressable scenes are loaded, load the login scene
        Debug.Log("Starting to load login scene");
        AsyncOperation loginSceneLoad = SceneManager.LoadSceneAsync(loginSceneName, LoadSceneMode.Single);

        while (!loginSceneLoad.isDone)
        {
            // Update progress bar for the login scene
            progressBar.value = (totalProgress + loginSceneLoad.progress) / (totalScenes + 1);
            Debug.Log($"Loading progress for login scene: {loginSceneLoad.progress}");
            yield return null;
        }

        if (loginSceneLoad.isDone)
        {
            Debug.Log("Successfully loaded login scene");
            // All scenes loaded and login scene is now active
            loadingScreen.SetActive(false);
        }
        else
        {
            Debug.LogError($"Failed to load login scene: {loginSceneName}");
        }
    }
}
