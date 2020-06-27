using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void onBackToMainMenuClick()
    {
        StartCoroutine(LoadYourAsyncScene("MainMenu"));
    }
    public void onBackToLoseSceneClick()
    {
        StartCoroutine(LoadYourAsyncScene("Lose"));
    }
    public void onBackToWinSceneClick()
    {
        StartCoroutine(LoadYourAsyncScene("Win"));
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
