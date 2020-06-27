using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private static ScoreTracker instance;

    [SerializeField]
    private Text scoreText;
    private static int score;

    public static int Score {
        get {
            return score;
        }
        set {
                score = value;
                updateScoreText(value);

                if(value >= 20 && SceneManager.GetActiveScene().name != "Win")
                {
                    loadScene();
                }
        } 
    }
    private void Awake()
    {
        instance = this;
        Score = 0;
    }

    public static void AddScore(int amount) => Score += amount;

    public void OnAddOneScoreButtonClick() => Score += 1;

    private static void loadScene()
    {
        instance.StartCoroutine(instance.LoadSceneASync());
    }

    private static void updateScoreText(int value)
    {
        instance.scoreText.text = "x " + value.ToString();
    }

    IEnumerator LoadSceneASync()
    {
        string scene = "Win";
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}