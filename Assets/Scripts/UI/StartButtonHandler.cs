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
    public AssetReference[] assetReferences; // Array of asset references to load
    public string loginSceneName; // Name of the login scene in the build settings

    public void OnStartButtonPressed()
    {
        // Show the loading screen
        loadingScreen.SetActive(true);
        Debug.Log("Loading screen activated");

        // Start loading assets
        StartCoroutine(LoadAssetsInBackground());
    }

    private IEnumerator LoadAssetsInBackground()
    {
        float totalProgress = 0f;
        int totalAssets = assetReferences.Length;

        for (int i = 0; i < totalAssets; i++)
        {
            Debug.Log($"Starting to load asset {assetReferences[i].RuntimeKey}");
            var handle = assetReferences[i].LoadAssetAsync<Object>();

            while (!handle.IsDone)
            {
                // Update progress bar
                progressBar.value = (totalProgress + handle.PercentComplete) / totalAssets;
                Debug.Log($"Loading progress for {assetReferences[i].RuntimeKey}: {handle.PercentComplete}");
                yield return null;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                totalProgress += 1f;
                Debug.Log($"Successfully loaded asset {assetReferences[i].RuntimeKey}");
                // Optionally store or use the loaded asset here
                var loadedAsset = handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load asset: {assetReferences[i].RuntimeKey}");
            }
        }

        // Once all addressable assets are loaded, load the login scene
        Debug.Log("Starting to load login scene");
        AsyncOperation loginSceneLoad = SceneManager.LoadSceneAsync(loginSceneName, LoadSceneMode.Single);

        while (!loginSceneLoad.isDone)
        {
            // Update progress bar for the login scene
            progressBar.value = (totalProgress + loginSceneLoad.progress) / (totalAssets + 1);
            Debug.Log($"Loading progress for login scene: {loginSceneLoad.progress}");
            yield return null;
        }

        if (loginSceneLoad.isDone)
        {
            Debug.Log("Successfully loaded login scene");
            // All assets loaded and login scene is now active
            loadingScreen.SetActive(false);
        }
        else
        {
            Debug.LogError($"Failed to load login scene: {loginSceneName}");
        }
    }
}
